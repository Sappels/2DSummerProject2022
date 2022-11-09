using UnityEngine;
using Touch = UnityEngine.Touch;

public class UseAbilitiesWithTouch : MonoBehaviour 
{
    public float timerThreshold;
    public Vector2 dashDirection;
    bool justDashed;
    bool counting;
    float timer;

    PlayerMovement playerMovement;
    private Vector2 touchStart;
    private Vector2 touchEnd;

    private void DashSwipe()
    {
        if (Input.touchCount < 1) return;

        Touch t = Input.GetTouch(0);
        if (t.position.x < Screen.width / 2) return;

        if (t.phase == TouchPhase.Began)
            touchStart = touchEnd = t.position;
        if (t.phase == TouchPhase.Moved)
        {
            touchEnd = t.position;
            Vector2 diff = touchEnd - touchStart;
            diff = new Vector2(diff.x / Screen.width, diff.y / Screen.height); // Normalize the difference relative to screen space
            if (diff.magnitude > 0.1f) // Is the swipe 10% or more of the screen?
            {
                touchStart = touchEnd; // Change the swipe starting position.
                dashDirection = diff.normalized;
                SendMessage("MidAirDash");
                
                justDashed = true;
            }
        }
    }

    private void JumpAndJetPack()
    {
        //start timer
        if (counting) timer += Time.deltaTime;
        else if (!counting)
            timer = 0;
        playerMovement.isJetPackTurnedOn = false;

        //check to start jetpack // CHECK WITH TESTERS
        if (timer > timerThreshold && !playerMovement.isGrounded)
            playerMovement.isJetPackTurnedOn = true;
        else
            playerMovement.isJetPackTurnedOn = false;

        //Loop to check touch information
        foreach (Touch touch in Input.touches)
        {
            Vector2 startPos;

            if ((touch.position.x > Screen.width / 2) && (touch.phase == UnityEngine.TouchPhase.Began))
            {
                startPos = touch.position;
                counting = true;
            }

            if ((touch.position.x > Screen.width / 2) && (touch.phase == UnityEngine.TouchPhase.Ended))
            {
                //check if it was a tap
                if (timer < timerThreshold && !justDashed)
                {
                    SendMessage("Jump");
                }else if (timer > timerThreshold && playerMovement.isGrounded)
                {
                    //THIS IS A TEST CHECK WITH TESTERS
                    SendMessage("Jump");
                }

                counting = false;
                justDashed = false;
            }
        }
    }

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        JumpAndJetPack();
        DashSwipe();
    }
}