using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreDisplay : MonoBehaviour
{
    public TMP_Text highscoreText;
    public TMP_Text airTimeHighScoreText;
    public TMP_Text totalBeansCollectedText;

    private void Start()
    {
        StartCoroutine(DisplayScoresOnMainMenu());
    }

    private IEnumerator DisplayScoresOnMainMenu()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        highscoreText.text = "Highscore: " + SaveManager.Instance.highscore;
        airTimeHighScoreText.text = "Airtime Highscore: " + SaveManager.Instance.airtimeHighscore;
        totalBeansCollectedText.text = "Total beans collected: " + SaveManager.Instance.candybeans;
    }
}
