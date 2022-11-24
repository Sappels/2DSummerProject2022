using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffWhenGameStart : MonoBehaviour
{


    float lerpDuration = 0.5f;
    float startValue = 1f;
    float endValue = 0f;
    public float alphaValue = 1f;

    public void StartFade()
    {
        StartCoroutine(FadeLerp());
    }

    IEnumerator FadeLerp()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            alphaValue = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        alphaValue = endValue;
    }

}
