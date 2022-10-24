using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFx : MonoBehaviour
{
    public void FlipAndMirror()
    {
        Camera.main.projectionMatrix = Camera.main.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, -1, 1));
    }

}
