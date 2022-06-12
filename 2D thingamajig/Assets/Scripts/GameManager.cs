using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private CameraShake cameraShake;
    [SerializeField] TMP_Text scoreText;

    private PlayerMovement playerMovement;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        Application.targetFrameRate = 120;
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        ScoreFx();

        scoreText.text = "Score: " + score;
    }

    private void ScoreFx()
    {

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
