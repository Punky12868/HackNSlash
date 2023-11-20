using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AimRotation : MonoBehaviour
{
    Player player;

    public Transform aimOrientation;
    [SerializeField] Transform orientation;

    [SerializeField] float rotationSpeed;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }
    private void Update()
    {
        float horiz = player.GetAxisRaw("Move Weapon H");
        float vert = player.GetAxisRaw("Move Weapon V");

        if (WeaponCombo.canAim && !CameraOrbit.orbiting)
        {
            if (GetCurrentInput.isMouseInput)
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
