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
    [SerializeField] GameObject EndGameScreen;
    [SerializeField] TextMeshProUGUI Line1Text;
    [SerializeField] TextMeshProUGUI Line2Text;
    [SerializeField] TextMeshProUGUI scoreText;

    public float TotalWeightGain = 0;
    public float TotalWeightLoss = 0;

    public enum eEndStates
    {
        WIN,
        STARVE,
        OVERFED
    }

    // Start is called before the first frame update
    void Start()
    {
        goalSlider.maxValue = endDistance.position.y;
        EndGameScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        goalSlider.value = catPosition.position.y;
        if (catPosition.position.y >= endDistance.position.y) EndGame(eEndStates.WIN);

    }

    public void EndGame(eEndStates endState)
    {
        Time.timeScale = 0;
        scoreText.text = TotalWeightGain.ToString("00.00") + "  -  " + TotalWeightLoss.ToString("00.00");
        EndGameScreen.SetActive(true);
        switch (endState)
        {
            case eEndStates.WIN:
                Line1Text.text = "You did it!";
                Line2Text.text = "But at what cost?";
                break;
            case eEndStates.STARVE:
                Line1Text.text = "Oh No!";
                Line2Text.text = "The cat starved to death...";
                break;
            case eEndStates.OVERFED:
                Line1Text.text = "Oh No!";
                Line2Text.text = "The cat was overfed...";
                break;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
