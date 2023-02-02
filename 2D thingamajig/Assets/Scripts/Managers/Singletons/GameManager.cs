using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System;
using System.Net.Sockets;

public class GameManager : MonoBehaviour
{
    public bool hasGameStarted = false;
    public bool shakeHarder;
    public bool isGamepadConnected;
    public float gameSpeed;

    public int score;
    public int airScoreHighestSingle;
    public int highestMultiplier;
    public int airScoreTotal;

    public int highscore;

    private CameraShake cameraShake;
    public TMP_Text scoreText;

    public PlayerMovement playerMovement;

    public AdsInitializer adsManager;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    public delegate void ModCheckHundred();
    public static event ModCheckHundred modCheckHundredEvent;

    public delegate void ModCheckFifty();
    public static event ModCheckFifty modCheckFiftyEvent;

    public delegate void ModCheckTen();
    public static event ModCheckTen modCheckTenEvent;

    public delegate void GameStart();
    public static event GameStart gameStart;

    void Start()
    {

        Application.targetFrameRate = 120;
        hasGameStarted = false;
        
        if (SceneManager.GetActiveScene().name == "GameOver" && score >= adsManager.minimumScoreToShowAd)
            adsManager.ShowInterstitialAd();
    }

    private void FixedUpdate()
    {
        isGamepadConnected = Gamepad.all.Count < 1 ? false : true;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    public void ResetGame()
    {
        hasGameStarted = false;
        gameSpeed = 1f;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
        airScoreTotal = 0;
        airScoreHighestSingle = 0;
        highestMultiplier = 0;
    }

    public void AddScore(int scoreToFillUp, int scoreValue)
    {
        switch (scoreToFillUp)
        {
            case 1:
                Console.WriteLine("Case 1");
                score += scoreValue;
                ModulusChecks();
                break;
            case 2:
                Console.WriteLine("Case 2");
                airScoreTotal += scoreValue;
                break;
            case 3:
                default:
                break;
        }
        scoreText.text = "Beans: " + score;
    }

    private void PlayScoreFx(int modulusCheck)
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        if (score == 1) return;


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

    private void ModulusChecks()
    {
        if (!hasGameStarted)
            gameStart.Invoke();


        int modCheckOneHundred = score % 100;
        int modCheckFifty = score % 50;
        int modCheckTen = score % 10;
        int modCheckFive = score % 5;

        if (modCheckOneHundred == 0)
        {
            shakeHarder = true;
            modCheckHundredEvent.Invoke();
        }

        if (modCheckFifty == 0)
            modCheckFiftyEvent.Invoke();

        if (modCheckTen == 0)
            modCheckTenEvent.Invoke();

        PlayScoreFx(modCheckFive);
    }

    public void DestroyThyself() { Destroy(gameObject); }
}
