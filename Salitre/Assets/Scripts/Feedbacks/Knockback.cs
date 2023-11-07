using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public void MoveOnAttack(Transform hitDir, float force)
    {
        Vector3 moveDir = hitDir.forward;
        GetComponent<Rigidbody>().AddForce(moveDir.normalized * force, ForceMode.Impulse);
    }
    public void GetKnockback(Transform weapon, float knockbackForce)
    {
        Vector3 hitDir = (weapon.forward).normalized;
        GetComponent<Rigidbody>().AddForce(hitDir * knockbackForce, ForceMode.Impulse);
    }
}
