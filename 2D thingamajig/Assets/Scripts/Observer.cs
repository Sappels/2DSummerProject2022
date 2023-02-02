using System.Collections;
using UnityEngine;
using static CoinScript;

public class Observer : MonoBehaviour
{
    private ParticleSystem coinPfx;
    private PlayerMovement playerMovement;
    private Timer timer;
    private AirTimeScript airTimeScript;
    private MainMusic mainMusic;
    private AbilityIcons abilityIcons;

    public TurnOffWhenGameStart tutorialCanvas;
    public GameObject cameraRedPanel;

    public ParticleSystem coinSpecialPfx;
    public ParticleSystem animeSpeedLines;
    public ParticleSystem animeSpeedLines2;

    private void Start()
    {
        coinPfx = GameObject.Find("Coin").GetComponent<ParticleSystem>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        timer = GameObject.Find("TimerHolder").GetComponent<Timer>();
        airTimeScript = GetComponent<AirTimeScript>();
        mainMusic = GameObject.Find("MainMusic").GetComponent<MainMusic>();
        abilityIcons = GameObject.FindGameObjectWithTag("IconHolder").GetComponent<AbilityIcons>();
    }

    private void OnEnable()
    {
        GameManager.gameStart += StartTheGame;
        CoinScript.coinCollected += CoinBehaviour;
        PlayerMovement.justHitTheGround += AddAirTimePoints;
        PlayerMovement.justHitTheGround += AbilityIconAlphaReset;
        GameManager.modCheckHundredEvent += ActivateAnimeSpeedLines;
        GameManager.modCheckHundredEvent += IsItTimeToSpeedThingsUp;
        GameManager.modCheckFiftyEvent += PlayEnumerators;
        GameManager.modCheckFiftyEvent += DecreaseTimeToLose;
        GameManager.modCheckTenEvent += FillUpPlayerAbilities;
        GameManager.modCheckTenEvent += PlayCoinSpecialPFX;
        GameManager.modCheckTenEvent += AbilityIconAlphaReset;
    }

    private void OnDisable()
    {
        GameManager.gameStart -= StartTheGame;
        CoinScript.coinCollected -= CoinBehaviour;
        PlayerMovement.justHitTheGround -= AddAirTimePoints;
        PlayerMovement.justHitTheGround -= AbilityIconAlphaReset;
        GameManager.modCheckHundredEvent -= IsItTimeToSpeedThingsUp;
        GameManager.modCheckHundredEvent -= ActivateAnimeSpeedLines;
        GameManager.modCheckFiftyEvent -= PlayEnumerators;
        GameManager.modCheckFiftyEvent -= DecreaseTimeToLose;
        GameManager.modCheckTenEvent -= PlayCoinSpecialPFX;
        GameManager.modCheckTenEvent -= AbilityIconAlphaReset;
    }

    private void StartTheGame()
    {
        GameManager.Instance.hasGameStarted = true;
        mainMusic.audioSource.Play();
        tutorialCanvas.StartFade();
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.coolerCoinSound);
        PlayCoinSpecialPFX();
    }

    private void ActivateAnimeSpeedLines()
    {
        var emission = animeSpeedLines.emission;
        var emission2 = animeSpeedLines2.emission;

        if (GameManager.Instance.score <= 199)
        {
            emission.enabled = true;
            emission2.enabled = false;
        }
        else
        {
            emission.enabled = false;
            emission2.enabled = true;
        }
    }

    private void AbilityIconAlphaReset()
    {
        abilityIcons.ResetAll();
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

