using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject Target;

    private void Update()
    {
        transform.position = new Vector3(0, Target.transform.position.y + 2.0f, -10);
    }
}
