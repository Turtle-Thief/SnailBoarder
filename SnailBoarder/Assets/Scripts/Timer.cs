using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeForLevel = 0;
    private float timeRemaining = 60;
    public bool timerIsRunning = false;
    private TextMeshProUGUI timerText;

    public void SetAndStartTimer()
    {
        timerIsRunning = true;
        timeRemaining = timeForLevel;
    }

    // Start our timer to run for timerLength
    public void SetAndStartTimer(int timerLength)
    {
        timerIsRunning = true;
        timeForLevel = timerLength;
        timeRemaining = timeForLevel;
    }

    private void StopAndResetTimer()
    {
        timeRemaining = 0; // Reset our time
        timerIsRunning = false; // Stop the timer
    }

    private void Start()
    {
        // Set our timer amount to the amount determined by the level
        timeRemaining = timeForLevel;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!GameManager.instance.gameIsPaused)
        {
            if (timerIsRunning) // this might be redundant
            {
                if (timeRemaining > 0) // Did we run out of time?
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else // If so, ...
                {
                    Debug.Log("Timer is done");
                    GameManager.instance.OnFinishedLevel();
                    StopAndResetTimer();
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
