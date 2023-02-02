using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AirTimeScript : MonoBehaviour
{
    public float airPoints;
    public float rateMultiplier;

    [SerializeField] float pointsPerTick;
    [SerializeField] float timeToStartTick;
    private float timer;
    private float multiplierTimer;
    

    public TMP_Text scoreText;

    [SerializeField] PlayerMovement player;

    private void OnEnable()
    {
        scoreText.color = Color.white;
    }

    void Update()
    {
        if (!player.isGrounded) 
            timer += Time.deltaTime;
        else
        {
            timer = 0;
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
            scoreText.transform.localScale = new Vector3(1,1,1);
        }


        if (multiplierTimer > 5 && rateMultiplier < 64)
        {
            TextFlash();
            rateMultiplier *= 2;
            multiplierTimer = 0;
        }

        var roundedScore = (Mathf.Round(airPoints));
        if (rateMultiplier <= 1)
            scoreText.text = roundedScore.ToString();
        else
            scoreText.text = "X" + rateMultiplier.ToString() + ": " + roundedScore.ToString();

    }

    async void TextFlash()
    {
        await BlendColor("red");
        await BlendColor("white");
    }

    async Task BlendColor(string color)
    {
        float timer = Time.time + 0.25f;

        while (Time.time < timer)
        {
            float colorShift = ((timer - Time.time) * 10f);

            if (color == "red")
                scoreText.color = Color.HSVToRGB(0, 1 / colorShift, 1);
            else if (color == "white")
            {
                float timeToWhite = (timer - Time.time) * 2;
                if (timer - Time.time < 0.1f)
                    timeToWhite = 0;

                scoreText.color = Color.HSVToRGB(0, timeToWhite, 1);
            }

            await Task.Yield();
        }
    }
}
