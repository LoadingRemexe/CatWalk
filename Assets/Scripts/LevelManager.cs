using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Slider goalSlider;
    [SerializeField] Transform catPosition;
    [SerializeField] float endDistance = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        goalSlider.maxValue = endDistance;
    }

    // Update is called once per frame
    void Update()
    {
        goalSlider.value = catPosition.position.y;
    }
}
