using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] float knockbackForce;
    [SerializeField] float freezeTime;
    public void GetDamage(int damage, Transform weaponDir)
    {
        if (GetComponent<Health>().health > 0)
        {
            GetComponent<Health>().health -= damage;
            GetFeedback(weaponDir);
            Death();
        }
    }
    void Death()
    {
        if (GetComponent<Health>().health <= 0)
        {
            Destroy(GetComponent<Health>().gameObject);
        }
    }
    void GetFeedback(Transform hitDirection)
    {
        GetComponent<Knockback>().GetKnockback(hitDirection, knockbackForce);
        GetFreezeTime(freezeTime);
    }
    void GetFreezeTime(float i)
    {
        Freeze freezeFeedback = GetComponent<Freeze>();
        freezeFeedback.StartCoroutine(freezeFeedback.StartFreeze(i));
    }
}
