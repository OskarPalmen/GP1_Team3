using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI highScore;

    [Header("Values")]
    public float currentHighScore;
    public float enemyScore;
    public float progressScore;


    void Start()
    {
        
    }

    void Update()
    {
        float currentProgress = 1f; //add distance calculation here
        if (currentProgress > progressScore)
        {
            progressScore = currentProgress;
        }
        currentHighScore = progressScore + enemyScore;
        highScore.text = currentHighScore.ToString("0");
        AddScore(1);
    }

    public void AddScore(int score)
    {
        enemyScore += score;
    }
}
