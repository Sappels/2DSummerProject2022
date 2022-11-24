using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeTextAtStartOfGame : MonoBehaviour
{
    private TurnOffWhenGameStart tutorialCanvas;
    private TextMeshProUGUI textMeshProUGUI;

    float initialAlphaValue;

    private void Start()
    {
        tutorialCanvas = GetComponentInParent<TurnOffWhenGameStart>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        initialAlphaValue = textMeshProUGUI.alpha;
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
        
        newColor = textMeshProUGUI.color;
        newColor.a = tutorialCanvas.alphaValue;
        textMeshProUGUI.color = newColor;

    }

}
