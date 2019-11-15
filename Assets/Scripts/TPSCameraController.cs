using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
    //TODO : finish this first ?
    public Transform target;
    public Vector3 offset;

    public float pitch = 2;

    void LateUpdate()
    {
        transform.position = target.position - offset;
        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
