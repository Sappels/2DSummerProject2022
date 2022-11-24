using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static CoinScript;

public class Observer : MonoBehaviour
{
    private ParticleSystem coinPfx;
    private PlayerMovement playerMovement;
    private Timer timer;
    private AirTimeScript airTimeScript;
    private MainMusic mainMusic;

    public TurnOffWhenGameStart tutorialCanvas;
    public GameObject cameraRedPanel;

    public ParticleSystem coinSpecialPfx;

    private void Start()
    {
        coinPfx = GameObject.Find("Coin").GetComponent<ParticleSystem>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        timer = GameObject.Find("TimerHolder").GetComponent<Timer>();
        airTimeScript = GetComponent<AirTimeScript>();
        mainMusic = GameObject.Find("MainMusic").GetComponent<MainMusic>();
    }

    private void OnEnable()
    {
        GameManager.gameStart += StartTheGame;
        CoinScript.coinCollected += CoinBehaviour;
        PlayerMovement.justHitTheGround += AddAirTimePoints;
        GameManager.modCheckHundredEvent += IsItTimeToSpeedThingsUp;
        GameManager.modCheckFiftyEvent += PlayEnumerators;
        GameManager.modCheckFiftyEvent += DecreaseTimeToLose;
        GameManager.modCheckTenEvent += FillUpPlayerAbilities;
        GameManager.modCheckTenEvent += PlayCoinSpecialPFX;
    }

    private void OnDisable()
    {
        GameManager.gameStart -= StartTheGame;
        CoinScript.coinCollected -= CoinBehaviour;
        PlayerMovement.justHitTheGround -= AddAirTimePoints;
        GameManager.modCheckHundredEvent -= IsItTimeToSpeedThingsUp;
        GameManager.modCheckFiftyEvent -= PlayEnumerators;
        GameManager.modCheckFiftyEvent -= DecreaseTimeToLose;
        GameManager.modCheckTenEvent -= PlayCoinSpecialPFX;
    }

    private void StartTheGame()
    {
        GameManager.Instance.hasGameStarted = true;
        mainMusic.audioSource.Play();
        tutorialCanvas.StartFade();
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.coolerCoinSound);
        PlayCoinSpecialPFX();
    }



    private void IsItTimeToSpeedThingsUp()
    {
        if (GameManager.Instance.score <= 200)
        {
            GameManager.Instance.gameSpeed += 0.1f;
            playerMovement.SpeedThingsUp(GameManager.Instance.gameSpeed);
        }
    }
    private void DecreaseTimeToLose()
    {
        if (GameManager.Instance.score <= 200)
        {
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.timeDecreaseSound);
            timer.resetTime -= 0.5f;
        }
    }

    private void CoinBehaviour()
    {
        PlayCoinPFX();
        AddPoints(1, 1);
    }

    private void PlayEnumerators()
    {
        StartCoroutine(ColorTint());
    }

    private void PlayCoinPFX()
    {
        coinPfx.Emit(300);
    }

    private void PlayCoinSpecialPFX()
    {
        coinSpecialPfx.Emit(30000);
    }

    private void AddAirTimePoints()
    {
        AddPoints(2, (int)airTimeScript.airPoints);

        if (airTimeScript.airPoints > GameManager.Instance.airScoreHighestSingle) 
            GameManager.Instance.airScoreHighestSingle = (int)airTimeScript.airPoints;

        if (airTimeScript.rateMultiplier > GameManager.Instance.highestMultiplier)
            GameManager.Instance.highestMultiplier = (int)airTimeScript.rateMultiplier;
        
        airTimeScript.airPoints = 0;
        airTimeScript.rateMultiplier = 0;
    }

    private void AddPoints(int scoreValue, int scoreToFillUp)
    {
        GameManager.Instance.AddScore(scoreValue, scoreToFillUp);
    }
    private void FillUpPlayerAbilities()
    {
        playerMovement.refillAll();
    }

    private IEnumerator ColorTint()
    {
        cameraRedPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        cameraRedPanel.SetActive(false);
    }
}

