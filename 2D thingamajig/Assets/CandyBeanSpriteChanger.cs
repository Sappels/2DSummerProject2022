using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBeanSpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }
}
