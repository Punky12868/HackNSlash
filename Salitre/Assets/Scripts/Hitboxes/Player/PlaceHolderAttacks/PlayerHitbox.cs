using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public LayerMask Hittable;
    BoxCollider boxCollider;

    bool hit;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        FindObjectOfType<SpawnHitbox>().currentHitbox = this.gameObject;

        Collider[] hitEnemies = Physics.OverlapBox(FindObjectOfType<SpawnHitbox>().orientation.position, boxCollider.size, FindObjectOfType<SpawnHitbox>().orientation.localRotation, Hittable);

        foreach (BoxCollider enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.gameObject.name);
            enemy.GetComponent<TakeDamage>().GetDamage(FindObjectOfType<WeaponController>().damage);
            enemy.GetComponent<TakeDamage>().GetKnockback(FindObjectOfType<PlayerController>().gameObject.transform);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, boxCollider.size);
    }
}
