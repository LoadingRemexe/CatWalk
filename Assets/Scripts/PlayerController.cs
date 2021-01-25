using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public Vector2 GetWorldPoint2D()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector2 GetWorldPoint2D(Vector3 pos)
    {
        return Camera.main.ScreenToWorldPoint(pos);
    }
}
