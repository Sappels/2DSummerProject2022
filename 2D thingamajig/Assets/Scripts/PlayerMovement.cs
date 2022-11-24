using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Vectors")]
    public Vector2 moveDir;


    [Header("Floats")]
    [SerializeField] float dashPower;
    [SerializeField] float jumpPower;
    [SerializeField] float jetPower;
    [SerializeField] float fuelDrain;
    [SerializeField] float groundSpeed;
    [SerializeField] float airSpeed;
    [SerializeField] float speed;
    [SerializeField] float groundCheckRayLength;



    [Header("Other")]
    [SerializeField] LayerMask groundLayer;
    public bool isGrounded;
    public bool isJetPackTurnedOn;
    [SerializeField] ParticleSystem jetPackFx;
    [SerializeField] ParticleSystem DoubleJumpFx;
    [SerializeField] ParticleSystem dust;

    [Header("Slider UI")]
    [SerializeField] Image JumpSliderUI;
    [SerializeField] Image FuelSliderUI;
    [SerializeField] Image DashSliderUI;

    private Rigidbody2D rb2d;
    private Vector2 mousePos;
    private float jetFuel;
    private int dashesLeft;
    private int jumpsLeft;

    //Input stuff
    private PlayerInput playerInput;
    private InputAction dashInput;
    private InputAction jumpInput;
    private InputAction moveInput;
    private InputAction jetPackInput;

    public CustomOnScreenStick touchMovement;
    public UseAbilitiesWithTouch useAbilitiesWithTouch;

    public delegate void JustHitTheGround();
    public static event JustHitTheGround justHitTheGround;

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

        dashInput.performed += MidAirDashInput;
        jumpInput.performed += Jump;

        jetPackInput.started += FlipJetPackBool;
        jetPackInput.canceled += FlipJetPackBool;
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

    private void Update()
    {
        FlipPlayerInMoveDirection();

        if (moveDir.x > 0) CreateDust();
    }

    void FixedUpdate()
    {
        GetMousePos();
        Movement();
        JetPack();
        IncreaseFallSpeedOverTime();

        //Stupid UI stuff
        JumpSliderUI.fillAmount = (jumpsLeft / 2f);
        DashSliderUI.fillAmount = dashesLeft;

        int fuelLeft = (int)jetFuel;
        FuelSliderUI.fillAmount = (fuelLeft / 100f);
    }
    private void FlipPlayerInMoveDirection()
    {
        switch (moveDir.x)
        {
            case > 0:
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                break;
            case < 0:
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                break;
        }
    }
    void Movement()
    {
        Vector2 mDir = moveDir;
        mDir.y = 0;
        rb2d.AddForce(speed * mDir * GameManager.Instance.gameSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
    void MidAirDashInput(InputAction.CallbackContext obj)
    {
        MidAirDash();
    }
    public void MidAirDash()
    {
        Vector2 dashDir;
        //Checks if the player has a gamepad connected or not to determine how the dash should work
        if (touchMovement.gameObject.activeSelf) {dashDir = useAbilitiesWithTouch.dashDirection;}
        else if (GameManager.Instance.isGamepadConnected) {dashDir = moveDir;}
        else{dashDir = (Vector3)mousePos - transform.position;}

        Vector3 vel = rb2d.velocity;
        if (!isGrounded && dashesLeft > 0)
        {
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.dashSound);
            vel.y = 0;
            rb2d.gravityScale = 6;
            rb2d.velocity = vel;
            dashesLeft--;
            rb2d.AddForce(dashPower * dashDir.normalized, ForceMode2D.Impulse);
        }
    }
    private void Jump()
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
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.jumpSound);
            DoubleJumpFx.Emit(1000);
            vel.y = 0;
            rb2d.gravityScale = 6;
            rb2d.velocity = vel;
            jumpsLeft--;
            rb2d.AddForce(jumpPower * Vector2.up);
        }
    }
    public void Jump(InputAction.CallbackContext obj)
    {
        Jump();
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

    void FlipJetPackBool(InputAction.CallbackContext obj)
    {
        isJetPackTurnedOn = !isJetPackTurnedOn;
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

    private void CreateDust()
    {
        if (isGrounded)
            dust.Play();
        else
            dust.Stop();
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 1f);

        if (!isGrounded) return;

        transform.rotation = Quaternion.identity;
        justHitTheGround.Invoke();
        jetFuel = 100;
        jumpsLeft = 2;
        dashesLeft = 1;
        speed = groundSpeed;
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
    }

    private void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundCheck();
        rb2d.gravityScale = 6;
    }
}