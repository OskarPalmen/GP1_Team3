using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [Header("Component")]
    public HighScore highScore;
    public Timer timer;
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;
    public TextMeshProUGUI timeMultiplier;
    public TextMeshProUGUI totalScore;

    [Header("Values")]
    public float scoreValue;
    public float timeValue;
    public float timeMultiValue;
    public float totalScoreValue;

    [Header("Settings")]
    public float timeBreakpoint = 600f;

    void Start()
    {
        highScore = FindObjectOfType<HighScore>();
        timer = FindObjectOfType<Timer>();
        WinScreen scoreScreen = new WinScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWin()
    {
        Time.timeScale = 0f;
        scoreValue = highScore.GetScore();
        timeValue = timer.GetTime();
        timeMultiValue = (timeValue - timeBreakpoint) / timeBreakpoint;
        if (timeMultiValue >= 0)
        {
            timeMultiValue = 1f;
        }
        else
        {
            timeMultiValue *= -10;
        }
        
        totalScoreValue = scoreValue * timeMultiValue;

        int seconds = (int)(timeValue % 60);
        int minutes = (int)(timeValue / 60) % 60;
        //int hours = (int)(currentTime / 3600) % 24;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        score.text = scoreValue.ToString("0");
        time.text = timerString;
        timeMultiplier.text = timeMultiValue.ToString("0.00");
        totalScore.text = totalScoreValue.ToString("0");
    }
}
