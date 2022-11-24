using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpriteChanger : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    [SerializeField] Sprite[] backgrounds;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
    }

}
