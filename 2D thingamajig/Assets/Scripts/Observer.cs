using System.Collections;
using UnityEngine;
using static CoinScript;

public class Observer : MonoBehaviour
{
    private ParticleSystem coinPfx;
    public GameObject cameraRedPanel;

    private void Start()
    {
        coinPfx = GameObject.Find("Coin").GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        CoinScript.coinCollected += CoinBehaviour;
        GameManager.difficultyRaise += PlayEnumerators;
    }

    private void OnDisable()
    {
        CoinScript.coinCollected -= CoinBehaviour;
        GameManager.difficultyRaise -= PlayEnumerators;
    }

    private void CoinBehaviour()
    {
        PlayCoinPFX();
        AddPoints();
    }

    private void PlayEnumerators()
    {
        StartCoroutine(ColorTint());
    }

    private void PlayCoinPFX()
    {
        coinPfx.Emit(300);
    }

    private void AddPoints()
    {
        GameManager.Instance.AddScore(1);
    }

    private IEnumerator ColorTint()
    {
        cameraRedPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        cameraRedPanel.SetActive(false);
    }
}
