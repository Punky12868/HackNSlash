using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderRotation : MonoBehaviour
{
    [SerializeField] Transform aimDir;
    [SerializeField] float rotationSpeed;
    private void Update()
    {
        Vector3 facingPos = aimDir.position;
        Vector3 lookDirection = facingPos - transform.position;
        lookDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
