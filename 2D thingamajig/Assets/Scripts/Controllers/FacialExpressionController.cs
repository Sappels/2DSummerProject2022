using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressionController : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ChangeSpriteEveryFewSeconds());
    }

    private IEnumerator ChangeSpriteEveryFewSeconds()
    {
        float secondsToWait = Random.Range(2f, 5f);
        int spriteToChoose = Random.Range(0, sprites.Count + 1);
        yield return new WaitForSecondsRealtime(secondsToWait);
        spriteRenderer.sprite = sprites[spriteToChoose];
        StartCoroutine(ChangeSpriteEveryFewSeconds());
    }
}
