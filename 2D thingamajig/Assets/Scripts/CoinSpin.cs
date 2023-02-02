using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    float spinSpeed = 1;


    void Update()
    {
        transform.Rotate(0, 0, 180 * spinSpeed * Time.deltaTime);
    }
    public IEnumerator SpeedUpSpin()
    {
        if (DOTween.IsTweening(transform)) yield return null;

        DOVirtual.Float(1, 5, 0.5f, v => spinSpeed = v);
        yield return new WaitForSecondsRealtime(0.5f);
        DOVirtual.Float(5, 1, 0.5f, v => spinSpeed = v);
    }

}
