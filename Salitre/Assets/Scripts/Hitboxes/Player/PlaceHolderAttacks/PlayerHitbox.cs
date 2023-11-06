using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public LayerMask Hittable;
    [SerializeField] BoxCollider boxCollider;
    Vector3 pos;
    Quaternion rot;
    bool drawWire;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        FindObjectOfType<SpawnHitbox>().currentHitbox = this.gameObject;

        pos = FindObjectOfType<SpawnHitbox>().hitboxSpawnPoint.position;
        rot = FindObjectOfType<SpawnHitbox>().hitboxSpawnPoint.rotation;

        //Physics.SyncTransforms();

        Collider[] hitEnemies = Physics.OverlapBox(pos, boxCollider.size, rot, Hittable);

        foreach (BoxCollider enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.gameObject.name);
            enemy.GetComponent<TakeDamage>().GetDamage(FindObjectOfType<WeaponController>().damage);
            enemy.GetComponent<TakeDamage>().GetKnockback(FindObjectOfType<WeaponCombo>().knockbackOrientation);
        }
    }
    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawWireCube(Vector3.zero, boxCollider.size);*/

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(pos, rot, boxCollider.size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}