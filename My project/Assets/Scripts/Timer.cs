using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;


    void Start()
    {
        
    }

    
    void Update()
    {
        currentTime += Time.deltaTime;

        int seconds = (int)(currentTime % 60);
        int minutes = (int)(currentTime / 60) % 60;
        //int hours = (int)(currentTime / 3600) % 24;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timerString;
    }
}
