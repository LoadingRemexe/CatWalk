using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CatAI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI WeightLabel;

    Rigidbody2D rb2d;
    public Vector3 Destination;
    float Weight = 15.0f;
    float Speed = 15f;
    public float WeightLossRate = 0.02f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Destination = transform.position;
    }

    void FixedUpdate()
    {
        Vector3[] pelletLocations = FindObjectsOfType<FoodPellet>().Where(p=>p.isDropped).Select(p=>p.transform.position).ToArray();
        if (pelletLocations.Length > 0)
        {
            Destination = pelletLocations.OrderBy(p => (p - transform.position).magnitude).FirstOrDefault();
        }
        if (Vector3.Distance(Destination, transform.position) > .2f)
        {
            rb2d.MoveRotation(Quaternion.LookRotation(transform.position - Destination, Vector3.forward));
            rb2d.velocity = transform.up * Speed * Time.deltaTime;
            Weight -= WeightLossRate * Time.deltaTime;
        } else
        {
            rb2d.velocity = Vector2.zero;
        }
        WeightLabel.text = "Weight: " + Weight.ToString("00.00") ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FoodPellet food = collision.gameObject.GetComponent<FoodPellet>();
        if (food)
        {
            Debug.Log("Nom");
            Weight += food.WeightValue;
            food.Remove();
        }
    }
}
