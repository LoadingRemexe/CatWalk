using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    [SerializeField] float WeightValue = 1;
    [SerializeField] public int FoodAmmount = 1;

    public float GetWeight()
    {
        return WeightValue * FoodAmmount;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
