using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AimRotation : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [SerializeField] float rotationSpeed;

    private int playerID = 0;
    Rewired.Player player;
    private bool isMouseInput = true;
    private bool isControllerActive = false;
    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
    }
    private void Update()
    {
        float horiz = player.GetAxisRaw("Move Weapon H");
        float vert = player.GetAxisRaw("Move Weapon V");

        bool isControllerInput = Mathf.Abs(horiz) > 0.01f || Mathf.Abs(vert) > 0.01f;

        if (isControllerInput && !isControllerActive)
        {
            isMouseInput = false;
            isControllerActive = true;
        }

        if (!isControllerInput && isControllerActive)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                isMouseInput = true;
                isControllerActive = false;
            }
        }

        if (WeaponCombo.canAim)
        {
            if (isMouseInput)
            {
                RotateTowardsMouse();
            }
            else
            {
                RotateWithController(horiz, vert);
            }
        }
    }
    private void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = targetPoint - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
        //Debug.Log("MOUSE");
    }

    private void RotateWithController(float horiz, float vert)
    {
        if (horiz != 0 || vert != 0)
        {
            Vector3 direc = new Vector3(horiz, 0, vert);

            Vector3 camForward = orientation.forward;
            camForward.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(camForward);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * Quaternion.Euler(0, Mathf.Atan2(direc.x, direc.z) * Mathf.Rad2Deg, 0), Time.deltaTime * rotationSpeed);
        }
        //Debug.Log("CONTROLLER");
    }
}
