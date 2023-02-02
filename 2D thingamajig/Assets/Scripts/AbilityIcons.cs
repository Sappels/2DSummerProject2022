using UnityEngine;

public class AbilityIcons : MonoBehaviour
{
    public SpriteRenderer[] icons;

    private void Start()
    {
        ResetAll();
    }

    public void FadeJumpIcons(int jumpsLeft)
    {
        if (jumpsLeft <= 0) return;

        if (jumpsLeft == 1)
            LowerIconAlpha(icons[1]);
        else
            LowerIconAlpha(icons[0]);
    }

    public void LowerIconAlpha(SpriteRenderer icon)
    {
        Color faded = icon.color;
        faded.a = 0f;
        icon.color = faded;
    }

    public void ResetAll()
    {
        foreach (SpriteRenderer icon in icons)
        {
            Color normal = icon.color;
            normal.a = 1f;
            icon.color = normal;
        }
    }
}
