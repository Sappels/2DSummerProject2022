using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject followTarget;

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = followTarget.transform.position;
    }
}
