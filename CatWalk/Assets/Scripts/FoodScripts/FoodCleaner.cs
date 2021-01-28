using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCleaner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FoodPellet food = collision.GetComponent<FoodPellet>();
        if (food)
        {
            FoodBowl.Instance.AddFood(food.FoodAmmount);
            food.Remove();
            
        }
    }
}
