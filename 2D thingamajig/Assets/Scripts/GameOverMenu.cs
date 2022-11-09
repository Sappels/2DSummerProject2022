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
    [SerializeField] TMP_Text gameOverAirScoreText;

    private void Start()
    {


        retryButton.onClick.AddListener(() => { Retry(); });
        mainMenuButton.onClick.AddListener(() => { ReturnToMainMenu(); });

        gameOverScoreText.text = "Your score was: " + GameManager.Instance.score;
        gameOverAirScoreText.text = "Your airtime score was: " + GameManager.Instance.airScoreTotal;

        GameManager.Instance.DestroyThyself();
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
