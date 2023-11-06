using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private void Update()
    {
        if (WeaponCombo.canMove)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 lookDirection = cameraPosition - transform.position;
            lookDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection * -1);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
