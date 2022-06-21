using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool hasGameStarted = false;
    public int score;

    private CameraShake cameraShake;
    public TMP_Text scoreText;

    private PlayerMovement playerMovement;
    private Timer timer;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Application.targetFrameRate = 120;
        hasGameStarted = false;
        if (SceneManager.GetActiveScene().name == "GameScene");
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        scoreText.text = "Score: " + score;

    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        timer = GameObject.Find("TimerHolder").GetComponent<Timer>();
    }

    public void ResetGame()
    {
        hasGameStarted = false;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        PlayScoreFx();
        CheckIfTimeToDecreaseTimeToLose();

        scoreText.text = "Score: " + score;
    }

    private void CheckIfTimeToDecreaseTimeToLose()
    {
        int difficultyCheck = score % 50;
        if (difficultyCheck == 0 && score <= 200)
        {
            timer.resetTime -= 0.5f;
        }
    }

    private void PlayScoreFx()
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        int scoreToFullRefill = score % 10;
        if (scoreToFullRefill == 0)
        {
            playerMovement.refillAll();
        }

        int _modulatedScore = score % 5;

        if (_modulatedScore == 0)
        {
            cameraShake.CameraShakeFx();
            AudioManager.Instance.PlayCoolerCoinSound();
        }
        else
        {
            playerMovement.fuelRefill();
            AudioManager.Instance.PlayCoinSound();
        }
    }
}
