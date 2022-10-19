using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private Timer timer;
    [SerializeField] int coinValue;
    private ParticleSystem coinPfx;

    public delegate void CoinCollected();
    public static event CoinCollected coinCollected;

    private void Start()
    {
        timer = GameObject.Find("TimerHolder").GetComponent<Timer>();
        coinPfx = GetComponent<ParticleSystem>();
        
        SpawnInNewLocation();
    }

    private void SpawnInNewLocation()
    {
        Vector2 currentLocation = transform.position;
        Vector2 newLocation = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Vector2 comparePos = currentLocation - newLocation;
        if (comparePos.x < 2f || comparePos.y < 2f)
        {
            newLocation = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }

        transform.position = newLocation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            coinCollected?.Invoke();
            timer.timeLeft = timer.resetTime;
            SpawnInNewLocation();
        }
    }
}
