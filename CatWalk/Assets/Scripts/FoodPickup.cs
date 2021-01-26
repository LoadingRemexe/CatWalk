using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : FoodPellet
{
    public int FoodAmmount = 5;

    new void Start()
    {
        base.Start();
        WeightValue *= FoodAmmount;
        isDropped = true;
    }

    void OnMouseDown()
    {
        FindObjectOfType<FoodBowl>().AddFood(FoodAmmount);
        Remove();
    }
}
