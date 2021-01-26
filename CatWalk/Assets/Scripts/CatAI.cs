using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CatAI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI WeightLabel;
    [SerializeField] Transform HeadSightPoint;
    [SerializeField] LayerMask SightLayers;

    LevelManager levelManager;
    Rigidbody2D rb2d;
    public Vector3 Destination;
    float Weight = 15.0f;
    float Speed = 15f;
    public float WeightLossRate = 0.02f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Destination = transform.position;
        levelManager = LevelManager.Instance;
    }

    void FixedUpdate()
    {
        SearchForFood();
        if (Vector3.Distance(Destination, transform.position) > .1f)
        {
            rb2d.MoveRotation(Quaternion.LookRotation(transform.position - Destination, Vector3.forward));
            rb2d.velocity = transform.up * CalculateSpeed() * Time.deltaTime;
            Weight -= WeightLossRate * Time.deltaTime;
            levelManager.TotalWeightLoss += WeightLossRate * Time.deltaTime;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        WeightLabel.text = "Weight: " + Weight.ToString("00.00");
        if (Weight <= 0.0f)
        {
            levelManager.FailGame();
        }
    }

    float CalculateSpeed()
    {
        return Mathf.Clamp(Speed * (Speed / Weight), 0.2f, 25f);
    }

    void SearchForFood()
    {
        FoodPellet[] pelletLocations = FindObjectsOfType<FoodPellet>();
        if (pelletLocations.Length > 0)
        {
            List<Vector3> VisiblePellets = new List<Vector3>();
            foreach (FoodPellet pellet in pelletLocations)
            {
                if (pellet.isDropped)
                {
                    RaycastHit2D hit = Physics2D.Raycast(HeadSightPoint.position, (pellet.transform.position - HeadSightPoint.position), SightLayers);
                    if (hit && hit.collider.GetComponent<FoodPellet>())
                    {
                        VisiblePellets.Add(pellet.transform.position);
                    }
                }
            }
            if (VisiblePellets.Count > 0)
            {
                Destination = VisiblePellets.OrderBy(p => (p - transform.position).magnitude).FirstOrDefault();
            }
            else
            {
                Destination = transform.position;
            }
        }
        else
        {
            Destination = transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FoodPellet food = collision.gameObject.GetComponent<FoodPellet>();
        if (food)
        {
            Debug.Log("Nom");
            Weight += food.WeightValue;
            levelManager.TotalWeightGain += food.WeightValue;
            food.Remove();
            Destination = transform.position;
        }
    }
}
