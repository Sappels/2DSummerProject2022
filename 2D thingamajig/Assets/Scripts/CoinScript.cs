using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{

    private Timer timer;
    [SerializeField] int coinValue;
    private ParticleSystem coinPfx;

    private void Start()
    {
        timer = GameObject.Find("TimerHolder").GetComponent<Timer>();
        coinPfx = GetComponent<ParticleSystem>();
        GameManager.Instance.ResetGame();

        SpawnInNewLocation();
    }

    private void SpawnInNewLocation()
    {
        Vector2 newLocation =new Vector2(Random.Range(-20f, 20f), Random.Range(0f, 18f));
        transform.position = newLocation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            timer.timeLeft = timer.resetTime;
            coinPfx.Emit(300);
            GameManager.Instance.AddScore(coinValue);
            SpawnInNewLocation();
        }
    }
}
