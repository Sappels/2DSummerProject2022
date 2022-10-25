using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoyStickToTouch : MonoBehaviour
{
    public GameObject joystick;

    private void Start()
    {
        joystick.SetActive(false);
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            //Left side of screen
            if ((touch.position.x < Screen.width / 2) && (touch.phase == TouchPhase.Began))
            {
                Debug.Log("left");
                joystick.SetActive(true);
                joystick.transform.position = touch.position;

            }
            if ((touch.position.x < Screen.width / 2) && (touch.phase == TouchPhase.Ended))
            {
                joystick.SetActive(false);
            }
        }
    }
}
