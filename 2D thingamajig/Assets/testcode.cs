using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcode : MonoBehaviour
{
    public float scaleSpeed;

    void Start()
    {
        var sequence = DOTween.Sequence()
               .Join(transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), scaleSpeed, 5, 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
}
