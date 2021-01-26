using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    [SerializeField] Sprite droppedSprite;
    SpriteRenderer sr;
    public bool isDropped = false;
    public float WeightValue = 1f;

    protected void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Dropped()
    {
        isDropped = true;
        sr.sprite = droppedSprite;
        transform.parent = null;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
