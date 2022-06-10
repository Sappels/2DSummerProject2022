using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] int coinValue;

    private void Start()
    {
        SpawnInNewLocation();
    }

    private void Update()
    {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
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
            GameManager.Instance.AddScore(coinValue);
            AudioManager.Instance.PlayCoinSound();
            SpawnInNewLocation();
        }
    }
}
