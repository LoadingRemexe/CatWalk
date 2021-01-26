using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Slider goalSlider;
    [SerializeField] Transform catPosition;
    [SerializeField] float endDistance = 50.0f;
    [SerializeField] GameObject WinGameScreen;

    // Start is called before the first frame update
    void Start()
    {
        goalSlider.maxValue = endDistance;
        WinGameScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        goalSlider.value = catPosition.position.y;
        if (catPosition.position.y >= endDistance) WinGame();
    }

    void WinGame()
    {
        WinGameScreen.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
