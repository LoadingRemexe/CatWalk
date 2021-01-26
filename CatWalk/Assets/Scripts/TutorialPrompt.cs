using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPrompt : Singleton<TutorialPrompt>
{
    [SerializeField] float TimeVisible = 5.0f;
    [SerializeField] GameObject promptUI;
    [SerializeField] TextMeshProUGUI promptUIText;
    [SerializeField] string[] tutorialPrompts;

    int promptIndex;
    bool entered = false;
    float TimeCountdown = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            TimeCountdown -= Time.deltaTime;
        }
        if (TimeCountdown <= 0.0f)
        {
            promptUI.SetActive(false);
            entered = false;
        }
    }

    public void ShowNextPrompt()
    {
        entered = true;
        promptUIText.text = tutorialPrompts[promptIndex];
        promptUI.SetActive(true);
        TimeCountdown = TimeVisible;
        promptIndex++;
    }

}
