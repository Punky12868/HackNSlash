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
    CinemachineTrackedDolly _vDollyTrack;

    float mouseX;
    float scrollY;
    Vector3 values;

    [Header ("Global Settings")]

    float fov = 15;
    float storedFov;

    [Range(1, 15)]
    [SerializeField] float maxFov, minFov;

    [SerializeField] float lerpLockDuration;

    [SerializeField] float dollyMaxSpeed;

    [Header ("Controller Settings")]

    [Range(1, 1000)]
    [SerializeField] float controllerSens;

    [Range(1, 1000)]
    [SerializeField] float controllerLockSpeed;

    [Range(1, 30)]
    [SerializeField] float controllerSharpness;

    [Range(1, 30)]
    [SerializeField] float controllerDamping;

    [Range(1, 30)]
    [SerializeField] float controllerFovDamping;

    [Range(0.1f, 500)]
    [SerializeField] float controllerFovSens;

    [Header ("Mouse Settings")]

    [Range(1, 1000)]
    [SerializeField] float mouseSens;

    [Range(1, 1000)]
    [SerializeField] float mouseLockSpeed;

    [Range(1, 30)]
    [SerializeField] float mouseSharpness;

    [Range(1, 30)]
    [SerializeField] float mouseDamping;

    [Range(1, 30)]
    [SerializeField] float mouseFovDamping;

    [Range(0.1f, 500)]
    [SerializeField] float mouseFovSens;

    //-------------------SCRIPT SETTINGS-------------------------------
                                                                      //
    float sens, lockSpeed, sharpness, damping, fovDamping, fovSens;   //
                                                                      //
    //-----------------------------------------------------------------

    bool lockCameraPos;
    float cameraPos;
    private void Awake()
    {
        cameraPos = -0.5f;
        storedFov = fov;
        player = ReInput.players.GetPlayer(0);

        Cursor.lockState = CursorLockMode.Confined;
        _vDollyTrack = _vCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        _vCamera.m_Lens.OrthographicSize = fov;

        sens = controllerSens;
        lockSpeed = controllerLockSpeed;
        sharpness = controllerSharpness;
        damping = controllerDamping;
        fovDamping = controllerFovDamping;
        fovSens = controllerFovSens;
    }
    private void Update()
    {
        ChangeSettingsOnInput();
        GetInput();
        LockCamPos();
        ActivateCameraOrbit();
        FovController();
    }
    void ChangeSettingsOnInput()
    {
        if (GetCurrentInput.isMouseInput && sens != mouseSens)
        {
            sens = mouseSens;
            lockSpeed = mouseLockSpeed;
            sharpness = mouseSharpness;
            damping = mouseDamping;
            fovDamping = mouseFovDamping;
            fovSens = mouseFovSens;
        }
        else if (!GetCurrentInput.isMouseInput && sens != controllerSens)
        {
            sens = controllerSens;
            lockSpeed = controllerLockSpeed;
            sharpness = controllerSharpness;
            damping = controllerDamping;
            fovDamping = controllerFovDamping;
            fovSens = controllerFovSens;
        }
    }
    void GetInput()
    {
        scrollY = player.GetAxis("Camera Zoom");
        values = new Vector3(mouseX, scrollY, 0);

        if (player.GetButtonDown("Lock"))
        {
            if (!lockCameraPos)
            {
                lockCameraPos = true;
            }
        }
    }
    void LockCamPos()
    {
        if (lockCameraPos)
        {
            if (_vDollyTrack.m_PathPosition != 0 || _vCamera.m_Lens.OrthographicSize != storedFov)
            {
                if (_vCamera.m_Lens.OrthographicSize > storedFov - 0.25 && _vCamera.m_Lens.OrthographicSize < storedFov + 0.25)
                {
                    _vCamera.m_Lens.OrthographicSize = storedFov;
                    fov = storedFov;
                }
                else if (_vCamera.m_Lens.OrthographicSize != storedFov)
                {
                    _vCamera.m_Lens.OrthographicSize = Mathf.Lerp(_vCamera.m_Lens.OrthographicSize, storedFov, 1 - Mathf.Exp(-lerpLockDuration * 10 * Time.unscaledDeltaTime));
                    fov = _vCamera.m_Lens.OrthographicSize;
                }

                if (_vDollyTrack.m_PathPosition > -1.5 && _vDollyTrack.m_PathPosition < 1.5)
                {
                    _vDollyTrack.m_PathPosition = 0;
                }
                else
                {
                    if (_dollyCart.m_Position > 25)
                    {
                        _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, lockSpeed, 1 - Mathf.Exp(-lerpLockDuration * Time.unscaledDeltaTime));
                    }
                    else
                    {
                        _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, -lockSpeed, 1 - Mathf.Exp(-lerpLockDuration * Time.unscaledDeltaTime));
                    }
                }
            }
            else
            {
                lockCameraPos = false;
            }
        }
    }
    void ActivateCameraOrbit()
    {
        if (_vDollyTrack.m_PathPosition != cameraPos)
        {
            _vDollyTrack.m_PathPosition = Mathf.Lerp(_vDollyTrack.m_PathPosition, cameraPos, 1 - Mathf.Exp(-sharpness * 3 * Time.unscaledDeltaTime));
        }

        if (_dollyCart.m_Speed > dollyMaxSpeed)
        {
            _dollyCart.m_Speed = dollyMaxSpeed;
        }
        else if (_dollyCart.m_Speed < -dollyMaxSpeed)
        {
            _dollyCart.m_Speed = -dollyMaxSpeed;
        }

        if (player.GetButtonDown("Camera Orbit Right"))
        {
            //_dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, values.x * sens, 1 - Mathf.Exp(-sharpness * Time.unscaledDeltaTime));
            
            cameraPos -= 1;
        }
        else if (player.GetButtonDown("Camera Orbit Left"))
        {
            //_dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, 0, 1 - Mathf.Exp(-damping * Time.unscaledDeltaTime));
            cameraPos += 1;
        }
    }
    void FovController()
    {
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
        }
        else if (fov > maxFov)
        {
            fov = maxFov;
        }

        if (_vCamera.m_Lens.OrthographicSize != fov && !lockCameraPos)
        {
            _vCamera.m_Lens.OrthographicSize = Mathf.Lerp(_vCamera.m_Lens.OrthographicSize, fov, 1 - Mathf.Exp(-fovDamping * Time.unscaledDeltaTime));
        }
    }
}
