using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private int score;

    [SerializeField] TMP_Text scoreText;

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
        Application.targetFrameRate = 120;
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = "Score: " + score;
    }
}
