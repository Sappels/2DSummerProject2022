using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    public float resetTime;
    [SerializeField] TMP_Text timerText;

    private void Start()
    {
        resetTime = 5;
        timeLeft = resetTime;
    }

    void Update()
    {
        TimeToLose();
    }

    private void TimeToLose()
    {
        if (GameManager.Instance.hasGameStarted)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        var roundedTimer = (Mathf.Round(timeLeft * 100)) / 100.0;

        timerText.text = "Time: " + roundedTimer;
    }
}
