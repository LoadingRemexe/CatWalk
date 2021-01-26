using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Slider goalSlider;
    [SerializeField] Transform catPosition;
    [SerializeField] Transform endDistance;
    [SerializeField] GameObject WinGameScreen;
    [SerializeField] TextMeshProUGUI scoreText;

    public float TotalWeightGain = 0;
    public float TotalWeightLoss = 0;


    // Start is called before the first frame update
    void Start()
    {
        goalSlider.maxValue = endDistance.position.y;
        WinGameScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        goalSlider.value = catPosition.position.y;
        if (catPosition.position.y >= endDistance.position.y) WinGame();
    }

    void WinGame()
    {
        Time.timeScale = 0;
        scoreText.text = TotalWeightGain.ToString("00.00") + "  -  " + TotalWeightLoss.ToString("00.00");
        WinGameScreen.SetActive(true);
    }

    public void FailGame()
    {

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
