using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private float currentScore = 0;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake()
    {
        var countOfGameSession = FindObjectsOfType<GameController>().Length;
        if (countOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ShowCurrentLives();
        ShowCurrentScore();
    }
    private void ShowCurrentLives()
    {
        livesText.text = "Lives: " + playerLives.ToString();
    }
    private void ShowCurrentScore()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
    public void AddScore()
    {
        currentScore += CoinScript.FindObjectOfType<CoinScript>().CoinScore;
        ShowCurrentScore();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    private void TakeLife()
    {
        playerLives--;
        ShowCurrentLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
