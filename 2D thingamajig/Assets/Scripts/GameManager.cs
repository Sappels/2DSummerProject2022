using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool hasGameStarted = false;
    public bool shakeHarder;
    public int score;
    public float gameSpeed;

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
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ResetGame();
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
        gameSpeed = 1f;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        ModulusChecks();

        scoreText.text = "Score: " + score;
    }

    private void DecreaseTimeToLose()
    {
        if (score <= 200)
        {
            timer.resetTime -= 0.5f;
        }
    }

    private void IsItTimeToSpeedThingsUp()
    {
        if (score <= 200)
        {
            gameSpeed += 0.1f;
            playerMovement.SpeedThingsUp(gameSpeed);
        }
    }

    private void PlayScoreFx(int modulusCheck)
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

        if (modulusCheck == 0)
        {
            cameraShake.CameraShakeFx(0.5f);
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.coolerCoinSound);
        }
        else
        {
            playerMovement.fuelRefill();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.coinSound);
        }
    }

    private void FillUpPlayerAbilities()
    {
        playerMovement.refillAll();
    }

    private void ModulusChecks()
    {
        int modCheckOneHundred = score % 100;
        int modCheckFifty = score % 50;
        int modCheckTen = score % 10;
        int modCheckFive = score % 5;

        if (modCheckOneHundred == 0)
        {
            shakeHarder = true;
            IsItTimeToSpeedThingsUp();
        }

        if (modCheckFifty == 0)
        {
            DecreaseTimeToLose();
        }

        if (modCheckTen == 0)
        {
            FillUpPlayerAbilities();
        }

        PlayScoreFx(modCheckFive);
    }


    public void DestroyThyself() { Destroy(gameObject); }
}
