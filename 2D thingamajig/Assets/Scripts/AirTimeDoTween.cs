using DG.Tweening;
using TMPro;
using UnityEngine;

public class AirTimeDoTween : MonoBehaviour
{
    public float scaleDuration;
    public TMP_Text thisText;

    private void OnEnable()
    {
        var sequence = DOTween.Sequence()
               .Join(transform.DOBlendableScaleBy(new Vector3(0.5f, 0.5f, 0.5f), scaleDuration));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
