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

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text airScoreTotalText;
    [SerializeField] TMP_Text airScoreSingleText;
    [SerializeField] TMP_Text highestMultiplierText;

    private void Start()
    {
        retryButton.onClick.AddListener(() => { Retry(); });
        mainMenuButton.onClick.AddListener(() => { ReturnToMainMenu(); });

        scoreText.text = "Candy beans collected: " + GameManager.Instance.score;
        airScoreTotalText.text = "Total airtime score: " + GameManager.Instance.airScoreTotal;
        airScoreSingleText.text = "Highest airtime Single: " + GameManager.Instance.airScoreHighestSingle;

        if (GameManager.Instance.highestMultiplier >= 64)
            highestMultiplierText.text = "Highest multiplier: X" + GameManager.Instance.highestMultiplier + " (MAX VALUE!!)";
        else
            highestMultiplierText.text = "Highest multiplier: X" + GameManager.Instance.highestMultiplier;

    }

    private void Retry()
    {
        SceneManager.LoadScene("GameScene");
        GameManager.Instance.DestroyThyself();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.DestroyThyself();
    }
}
