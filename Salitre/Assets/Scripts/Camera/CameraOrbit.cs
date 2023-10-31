using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart _dollyCart;

    [SerializeField] float x;

    [Range(1, 1000)]
    [SerializeField] float sens;

    [Range(1, 30)]
    [SerializeField] float sharpness;

    [Range(1, 30)]
    [SerializeField] float damping;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Update()
    {
        x = Input.GetAxis("Mouse X") * sens;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, x, 1 - Mathf.Exp(-sharpness * Time.unscaledDeltaTime));

            //_dollyCart.m_Speed = Mathf.Clamp(Mathf.Lerp(_dollyCart.m_Speed, x, 1 - Mathf.Exp(-sharpness * Time.unscaledDeltaTime)), 0, _dollyCart.m_Path.PathLength);
            //_dollyCart.m_Position = Mathf.Clamp(Mathf.Lerp(_dollyCart.m_Position, x, 1-Mathf.Exp(-sharpness * Time.unscaledDeltaTime)), 0, _dollyCart.m_Path.PathLength);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            _dollyCart.m_Speed = Mathf.Lerp(_dollyCart.m_Speed, 0, 1 - Mathf.Exp(-damping * Time.unscaledDeltaTime));
        }
    }
}
