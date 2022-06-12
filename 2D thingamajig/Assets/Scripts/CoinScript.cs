using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] int coinValue;
    private ParticleSystem coinPfx;

    private void Start()
    {
        coinPfx = GetComponent<ParticleSystem>();

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
            coinPfx.Emit(300);
            GameManager.Instance.AddScore(coinValue);
            SpawnInNewLocation();
        }
    }
}