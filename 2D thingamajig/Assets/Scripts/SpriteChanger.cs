using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    [SerializeField] Sprite onGround;
    [SerializeField] Sprite offGround;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        ChangeSpriteWhileInAir();
    }

    private void ChangeSpriteWhileInAir()
    {
        if (playerMovement.isGrounded)
            spriteRenderer.sprite = onGround;
        else
            spriteRenderer.sprite = offGround;
    }
}
