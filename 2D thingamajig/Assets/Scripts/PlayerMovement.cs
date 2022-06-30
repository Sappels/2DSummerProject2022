using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] float dashPower;
    [SerializeField] float jumpPower;
    [SerializeField] float jetPower;
    [SerializeField] float fuelDrain;
    [SerializeField] float groundSpeed;
    [SerializeField] float airSpeed;
    [SerializeField] float speed;
    [SerializeField] float groundCheckRayLength;

    private float jetFuel;

    private int dashesLeft;
    private int jumpsLeft;
    private Vector2 moveDir;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isJetPackTurnedOn;
    [SerializeField] TMP_Text dashesLeftText;
    [SerializeField] TMP_Text jumpsLeftText;
    [SerializeField] TMP_Text fuelLeftText;
    [SerializeField] ParticleSystem jetPackFx;
    [SerializeField] ParticleSystem DoubleJumpFx;

    //Input stuff
    private PlayerInput playerInput;
    private InputAction dashInput;
    private InputAction jumpInput;
    private InputAction moveInput;
    private InputAction jetPackInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        
        dashInput = playerInput.Player.Dash;
        jumpInput = playerInput.Player.Jump;
        moveInput = playerInput.Player.Move;
        jetPackInput = playerInput.Player.JetPack;

        //Movement
        moveInput.performed += cntxt => moveDir = cntxt.ReadValue<Vector2>();
        moveInput.canceled += cntxt => moveDir = Vector2.zero;

        dashInput.performed += MidAirDash;
        jumpInput.performed += Jump;

        jetPackInput.started += StartJetPack;
        jetPackInput.canceled += TurnOffJetPack;
    }

    void Start()
    {
        dashesLeft = 1;
        jumpsLeft = 2;
        jetFuel = 100f;
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnEnable()
    {
        dashInput.Enable();
        jumpInput.Enable();
        moveInput.Enable();
        jetPackInput.Enable();
    }

    private void OnDisable()
    {
        dashInput.Disable();
        jumpInput.Disable();
        moveInput.Disable();
        jetPackInput.Disable();
    }

    void FixedUpdate()
    {
        Movement();
        JetPack();
        IncreaseFallSpeedOverTime();

        //Stupid UI stuff
        dashesLeftText.text = "D: " + dashesLeft.ToString();
        jumpsLeftText.text = "J: " + jumpsLeft.ToString();

        int fuelLeft = (int)jetFuel;
        fuelLeftText.text = "F: " + fuelLeft.ToString();
    }

    void Movement()
    {
        Vector2 mDir = moveDir;
        mDir.y = 0;
        rb2d.AddForce(speed * mDir * GameManager.Instance.gameSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
    void MidAirDash(InputAction.CallbackContext obj)
    {
        Vector3 vel = rb2d.velocity;
        if (!isGrounded && dashesLeft > 0)
        {
            AudioManager.Instance.PlayDashSound();
            vel.y = 0;
            rb2d.gravityScale = 6;
            rb2d.velocity = vel;
            dashesLeft--;
            rb2d.AddForce(dashPower * moveDir, ForceMode2D.Impulse);
        }
    }

    void Jump(InputAction.CallbackContext obj)
    {
        isGrounded = false;
        speed = airSpeed;
        Vector3 vel = rb2d.velocity;

        if (jumpsLeft > 0)
        {
            if (jumpsLeft == 1)
            {
                if (moveDir.x < 0)
                {
                    transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.LocalAxisAdd);
                }
                else
                {
                    transform.DORotate(new Vector3(0f, 0f, -360f), 1f, RotateMode.LocalAxisAdd);
                }
            }
            AudioManager.Instance.PlayJumpSound();
            DoubleJumpFx.Emit(1000);
            vel.y = 0;
            rb2d.gravityScale = 6;
            rb2d.velocity = vel;
            jumpsLeft--;
            rb2d.AddForce(jumpPower * Vector2.up);
        }
    }

    void JetPack()
    {
        if (isJetPackTurnedOn && jetFuel > 0)
        {
            jetPackFx.Play();
            //AudioManager.Instance.PlayJetPackSound();
            isGrounded = false;
            rb2d.gravityScale = 6;
            rb2d.AddForce(jetPower * Vector2.up * GameManager.Instance.gameSpeed * Time.deltaTime, ForceMode2D.Impulse);
            jetFuel -= Time.deltaTime * fuelDrain;
        }
        else
        {
            jetPackFx.Stop();
            //AudioManager.Instance.StopJetPackSound();
        }
    }

    void StartJetPack(InputAction.CallbackContext obj)
    {
        isJetPackTurnedOn = true;
    }

    void TurnOffJetPack(InputAction.CallbackContext obj)
    {
        isJetPackTurnedOn = false;
    }

    void IncreaseFallSpeedOverTime()
    {
        if (rb2d.velocity.y < 0 && !isGrounded)
        {
            if (rb2d.gravityScale < 20)
            {
                rb2d.gravityScale += 20 * GameManager.Instance.gameSpeed * Time.deltaTime;
            }
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 1f);

        if (isGrounded)
        {
            jetFuel = 100;
            jumpsLeft = 2;
            dashesLeft = 1;
            speed = groundSpeed;
        }
    }

    public void fuelRefill()
    {
        jetFuel += 10;
        if (jetFuel > 100)
        {
            jetFuel = 100;
        }
    }

    public void refillAll()
    {
        jetFuel = 100;
        jumpsLeft = 2;
        dashesLeft = 1;
    }

    public void SpeedThingsUp(float speedFactor)
    {
        dashPower *= speedFactor;
        //jumpPower *= speedFactor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("check");
        GroundCheck();
        rb2d.gravityScale = 6;
    }
}
