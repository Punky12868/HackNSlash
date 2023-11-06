using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Rewired;

public class CameraOrbit : MonoBehaviour
{
    Player player;

    [SerializeField] CinemachineVirtualCamera _vCamera;
    [SerializeField] CinemachineDollyCart _dollyCart;

    float mouseX;
    float scrollY;
    Vector3 values;

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

    [Range(0.1f, 500)]
    [SerializeField] float fovSens;

    [SerializeField] float lerpLockDuration;
    bool lockCameraPos;
    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);

        Cursor.lockState = CursorLockMode.Confined;
        _vCamera.m_Lens.FieldOfView = fov;
    }
    private void Update()
    {
        mouseX = player.GetAxis("Camera Orbit");
        scrollY = player.GetAxis("Camera Zoom");
        //normalizedValues = new Vector3(mouseX, scrollY, 0).normalized;
        values = new Vector3(mouseX, scrollY, 0);

        if (player.GetButtonDown("Lock"))
        {
            if (!lockCameraPos)
            {
                lockCameraPos = true;
            }
        }
        if (lockCameraPos)
        {
            if (_dollyCart.m_Position != 0)
            {
                if (_dollyCart.m_Position > -1.5 && _dollyCart.m_Position < 1.5)
                {
                    _dollyCart.m_Position = 0;
                    _dollyCart.m_Speed = 0;
                }
                else
                {
                    if (_dollyCart.m_Position > 50)
                    {
                        _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, sens, 1 - Mathf.Exp(-lerpLockDuration * Time.unscaledDeltaTime));
                    }
                    else
                    {
                        _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, -sens, 1 - Mathf.Exp(-lerpLockDuration * Time.unscaledDeltaTime));
                    }
                }
            }
            else
            {
                lockCameraPos = false;
            }
        }

        if (player.GetButton("Activate Orbit"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, values.x * sens, 1 - Mathf.Exp(-sharpness * Time.unscaledDeltaTime));

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
                fov += (values.y * fovSens) * -1;
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
