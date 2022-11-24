using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeImageAtStartOfGame : MonoBehaviour
{
    private TurnOffWhenGameStart tutorialCanvas;
    private Image image;

    float initialAlphaValue;

    private void Start()
    {
        tutorialCanvas = GetComponentInParent<TurnOffWhenGameStart>();
        image = GetComponent<Image>();

        initialAlphaValue = image.color.a;
    }

    private void Update()
    {
        if (GameManager.Instance.hasGameStarted)
            SetAlpha();
    }

    private void SetAlpha()
    {
        if (tutorialCanvas.alphaValue > initialAlphaValue) return;


        Color newColor;

        newColor = image.color;
        newColor.a = tutorialCanvas.alphaValue;
        image.color = newColor;

    }

}