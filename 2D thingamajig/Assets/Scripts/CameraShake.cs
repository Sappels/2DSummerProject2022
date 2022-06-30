using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public void CameraShakeFx(float shakeStrength)
    {
        if (GameManager.Instance.shakeHarder)
        {
            transform.DOShakePosition(shakeStrength * 3);
        }
        else
        {
            transform.DOShakePosition(shakeStrength);
        }

        GameManager.Instance.shakeHarder = false;
    }
}
