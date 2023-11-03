using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public enum EntityType {Hittable, Enemy, Player}
    public EntityType type;

    [SerializeField] float knockbackForce;
    public void GetDamage(int i)
    {
        switch (type)
        {
            case EntityType.Hittable:
                break;

            case EntityType.Enemy:

                if (GetComponent<EnemyController>().health > 0)
                {
                    GetComponent<EnemyController>().health -= i;
                    CheckHealth();
                }
                
                break;

            case EntityType.Player:

                if (GetComponent<PlayerController>().health > 0)
                {
                    GetComponent<PlayerController>().health -= i;
                    CheckHealth();
                }

                break;

            default:
                break;
        }
    }
    public void GetKnockback(Transform weapon)
    {
        Vector3 dir = (weapon.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(dir * -knockbackForce, ForceMode.Impulse);
    }
    void CheckHealth()
    {
        switch (type)
        {
            case EntityType.Hittable:
                break;
            case EntityType.Enemy:
                if (GetComponent<EnemyController>().health <= 0)
                {
                    Destroy(GetComponent<EnemyController>().gameObject);
                    
                }
                break;
            case EntityType.Player:
                if (GetComponent<PlayerController>().health <= 0)
                {
                    Destroy(GetComponent<PlayerController>().gameObject);

                }
                break;
            default:
                break;
        }
    }
}
