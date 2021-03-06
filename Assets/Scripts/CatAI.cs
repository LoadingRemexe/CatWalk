using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CatAI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI WeightLabel;
    [SerializeField] LayerMask SightLayers;
    [SerializeField] GameObject KittyPoo;

    Animator animator;
    LevelManager levelManager;
    Rigidbody2D rb2d;
    SpriteRenderer sr;
    public Vector3 Destination;
    float Weight = 15.0f;
    float Speed = 15f;
    public float WeightLossRate = 0.02f;
    float WeightDropCount = 0;
    float WeightDropCountReset = 2.0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Destination = transform.position;
        WeightDropCount = WeightDropCountReset;
        levelManager = LevelManager.Instance;
    }

    void FixedUpdate()
    {
        SearchForFood();
        if (Vector3.Distance(Destination, transform.position) > .1f)
        {
            //rb2d.MoveRotation(Quaternion.LookRotation(transform.position - Destination, Vector3.forward));
            //rb2d.velocity = transform.up * CalculateSpeed() * Time.deltaTime;
            rb2d.velocity = (Destination - transform.position).normalized * CalculateSpeed() * Time.deltaTime;
            Weight -= WeightLossRate * Time.deltaTime;
            WeightDropCount -= WeightLossRate * Time.deltaTime;
            levelManager.TotalWeightLoss += WeightLossRate * Time.deltaTime;

            sr.flipX = (transform.position.x > Destination.x);
            animator.SetBool("Walking", true);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            animator.SetBool("Walking", false);

        }

        WeightLabel.text = "Weight: " + Weight.ToString("00.00");
        int bodystageint = 1;
        if (Weight > 5.0f) bodystageint = 2;
        if (Weight > 10.0f) bodystageint = 3;
        animator.SetInteger("BodyStage", bodystageint);

        if (WeightDropCount <= 0)
        {
            Instantiate(KittyPoo, transform.position - transform.forward, Quaternion.identity, null);
            AudioManager.Instance.PlayRandomMeow();
            WeightDropCount = WeightDropCountReset;
        }
        if (Weight <= 0.0f)
        {
            levelManager.EndGame(LevelManager.eEndStates.STARVE);
        } 
        else if (Weight >= 30.0f)
        {
            levelManager.EndGame(LevelManager.eEndStates.OVERFED);
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

                RaycastHit2D hit = Physics2D.Raycast(transform.position, (pellet.transform.position - transform.position), SightLayers);
                if (hit && hit.collider.GetComponent<FoodPellet>())
                {
                    VisiblePellets.Add(pellet.transform.position);
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
            Weight += food.GetWeight();
            AudioManager.Instance.PlaySFX(3);
            levelManager.TotalWeightGain += food.GetWeight();
            food.Remove();
            Destination = transform.position;
        }
    }
}
