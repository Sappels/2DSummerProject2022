using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoyStickToTouch : MonoBehaviour, IPointerEnterHandler
{
    public GameObject joystick;

    private void Start()
    {
        joystick.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pressPosition.x < 0)
        {
            Debug.Log("The cursor entered the selectable UI element.");
        }
    }


}
