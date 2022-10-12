using UnityEngine;
using static CoinScript;

public class Observer : MonoBehaviour
{
    private ParticleSystem coinPfx;

    private void Start()
    {
        coinPfx = GameObject.Find("Coin").GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        CoinScript.coinCollected += CoinBehaviour;
    }

    private void OnDisable()
    {
        CoinScript.coinCollected -= CoinBehaviour;
    }

    private void CoinBehaviour()
    {
        PlayCoinPFX();
        AddPoints();
    }

    private void PlayCoinPFX()
    {
        coinPfx.Emit(300);
    }

    private void AddPoints()
    {
        GameManager.Instance.AddScore(1);
    }

}
