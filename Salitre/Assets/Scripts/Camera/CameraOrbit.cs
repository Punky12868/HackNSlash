using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _vCamera;
    [SerializeField] CinemachineDollyCart _dollyCart;

    float mouseX;
    float scrollY;

    [Range(1, 1000)]
    [SerializeField] float sens;

    [Range(1, 30)]
    [SerializeField] float sharpness;

    [Range(1, 30)]
    [SerializeField] float damping;

    float fov = 70;

    [Range(50, 120)]
    [SerializeField] float maxFov, minFov;

    [Range(1, 30)]
    [SerializeField] float fovDamping;

    [Range(1, 500)]
    [SerializeField] float fovSens;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _vCamera.m_Lens.FieldOfView = fov;
    }
    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sens;
        scrollY = Input.GetAxisRaw("Mouse ScrollWheel");

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, mouseX, 1 - Mathf.Exp(-sharpness * Time.unscaledDeltaTime));

            //_dollyCart.m_Speed = Mathf.Clamp(Mathf.Lerp(_dollyCart.m_Speed, x, 1 - Mathf.Exp(-sharpness * Time.unscaledDeltaTime)), 0, _dollyCart.m_Path.PathLength);
            //_dollyCart.m_Position = Mathf.Clamp(Mathf.Lerp(_dollyCart.m_Position, x, 1-Mathf.Exp(-sharpness * Time.unscaledDeltaTime)), 0, _dollyCart.m_Path.PathLength);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, 0, 1 - Mathf.Exp(-damping * Time.unscaledDeltaTime));
        }

        if (scrollY != 0)
        {
            if (fov >= minFov && fov <= maxFov)
            {
                fov += (scrollY * fovSens) * -1;
            }
        }

        if (fov < minFov)
        {
            fov = minFov;
            //_vCamera.m_Lens.FieldOfView = fov;
        }
        else if (fov > maxFov)
        {
            fov = maxFov;
            //_vCamera.m_Lens.FieldOfView = fov;
        }

        if (_vCamera.m_Lens.FieldOfView != fov)
        {
            _vCamera.m_Lens.FieldOfView = Mathf.Lerp(_vCamera.m_Lens.FieldOfView, fov, 1 - Mathf.Exp(-fovDamping * Time.unscaledDeltaTime));
        }
    }
}
