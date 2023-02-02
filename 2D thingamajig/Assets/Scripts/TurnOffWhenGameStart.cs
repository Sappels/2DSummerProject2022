using System.Collections;
using UnityEngine;

public class TurnOffWhenGameStart : MonoBehaviour
{
    float lerpDuration = 0.5f;
    float startValue = 1f;
    float endValue = 0f;
    public float alphaValue = 1f;

    public GameObject keyboardMouseText;
    public GameObject gamepadText;
    public GameObject mobileText;

    private void Update()
    {
        SetTutorialText();
    }

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

    private void SetTutorialText()
    {
        if (mobileText.activeInHierarchy) return;

        keyboardMouseText.SetActive(!GameManager.Instance.isGamepadConnected);
        gamepadText.SetActive(GameManager.Instance.isGamepadConnected);
    }

}
