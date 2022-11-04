using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AirTimePoints : MonoBehaviour
{
    public float airPoints;

    [SerializeField] float pointsPerTick;
    [SerializeField] float rateMultiplier;
    [SerializeField] float timeToStartTick;
    private float timer;
    private float multiplierTimer;

    public TMP_Text scoreText;

    [SerializeField] PlayerMovement player;

    void Update()
    {
        if (!player.isGrounded) 
        { 
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            airPoints = 0;
            rateMultiplier = 1;
        }


        if (timer > timeToStartTick)
        {
            scoreText.gameObject.SetActive(true);
            multiplierTimer += Time.deltaTime;
            airPoints += pointsPerTick * rateMultiplier * Time.deltaTime;
        }
        else
        {
            scoreText.gameObject.SetActive(false);
        }


        if (multiplierTimer > 5)
        {
            rateMultiplier *= 2;
            multiplierTimer = 0;
        }

        var roundedScore = (Mathf.Round(airPoints));
        scoreText.text = "X" + rateMultiplier.ToString() + ": " + roundedScore.ToString();
    }
}
