using TMPro;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.ResetGame();
        GameManager.Instance.scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        GameManager.Instance.playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GameManager.Instance.scoreText.text = "Beans: " + GameManager.Instance.score;
    }
}
