using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    [SerializeField] float WeightValue;
    [SerializeField] public int FoodAmmount;

    public float GetWeight()
    {
        return WeightValue * FoodAmmount;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
