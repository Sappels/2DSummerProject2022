using UnityEngine;
using UnityEngine.EventSystems;
[System.Serializable]
public enum JoyStickDirection { Horizontal, Vertical, Both }
public class CustomOnScreenStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public JoyStickDirection Direction = JoyStickDirection.Both;
    public RectTransform background;
    public RectTransform handle;
    [Range(0,2f)] public float handleLimit = 1f;
    Vector2 input = Vector2.zero;

    public PlayerMovement playerMovement;

    //Output
    public float Vertical { get { return input.y; } }
    public float Horizontal { get { return input.x; } }

    Vector2 JoyPosition = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        OnDrag(eventData);
        JoyPosition = eventData.position;
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 JoyDirection = eventData.position - JoyPosition;
        input = (JoyDirection.magnitude > background.sizeDelta.x / 2f)? JoyDirection.normalized : 
            JoyDirection / (background.sizeDelta.x / 2f);
        if (Direction == JoyStickDirection.Horizontal)
            input = new Vector2(input.x, 0f);
        if (Direction == JoyStickDirection.Vertical)
            input = new Vector2(0f, input.y);
        handle.anchoredPosition = (input * background.sizeDelta.x / 2f) * handleLimit;

        playerMovement.moveDir.x = Horizontal;
        playerMovement.moveDir.y = Vertical;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        playerMovement.moveDir = Vector2.zero;
        background.gameObject.SetActive(false);
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}