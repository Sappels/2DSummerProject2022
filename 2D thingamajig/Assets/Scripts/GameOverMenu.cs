using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public Button retryButton;
    public Button mainMenuButton;

    [SerializeField] TMP_Text gameOverScoreText;

    private void Start()
    {
        retryButton.onClick.AddListener(() => { Retry(); });
        mainMenuButton.onClick.AddListener(() => { ReturnToMainMenu(); });

        gameOverScoreText.text = "Your score was: " + GameManager.Instance.score;
    }

    private void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
