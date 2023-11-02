using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtCamera : MonoBehaviour
{
    CinemachineVirtualCamera _vCamera;
    private void Awake()
    {
        _vCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }
    private void LateUpdate()
    {
        var rotation = _vCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
