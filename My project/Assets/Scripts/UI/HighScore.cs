using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI highScore;
    public Transform playerPos;
    public Transform startPos;

    [Header("Values")]
    public float multiplier = 10f;
    public float currentHighScore;
    public float enemyScore;
    public float progressScore;
    //public float progressStart; //use if changeing scene


    void Start()
    {
        playerPos = FindObjectOfType<CharacterController>().transform;
        startPos = FindObjectOfType<SpawnPoint>().transform;

    }

    void Update()
    {
        float currentProgress = Vector3.Distance(startPos.position, playerPos.position); //distance calculation here
        currentProgress *= multiplier;
        if (currentProgress > progressScore)
        {
            progressScore = currentProgress;
        }
        currentHighScore = progressScore + enemyScore;
        highScore.text = currentHighScore.ToString("0");
    }

    public void AddScore(int score)
    {
        enemyScore += score;
    }

    public float GetScore()
    {
        return currentHighScore;
    }
}
