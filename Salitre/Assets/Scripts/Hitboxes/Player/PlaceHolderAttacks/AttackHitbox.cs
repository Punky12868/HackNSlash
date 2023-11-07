using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public LayerMask Hittable;
    [SerializeField] BoxCollider boxCollider;
    Vector3 pos;
    Quaternion rot;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        //FindObjectOfType<SpawnHitbox>().currentHitbox = this.gameObject;
        GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().currentHitbox = this.gameObject;

        pos = GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.position;
        rot = GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.rotation;

        Collider[] hitEntities = Physics.OverlapBox(pos, boxCollider.size, rot, Hittable);

        foreach (BoxCollider entity in hitEntities)
        {
            Debug.Log("Hit: " + entity.gameObject.name);

            Transform weaponDir = GetComponentInParent<AimRotation>().aimOrientation;
            int damage = GetComponentInParent<AimRotation>().GetComponentInChildren<Weapon>().damage;

            entity.gameObject.GetComponent<TakeDamage>().GetDamage(damage, weaponDir);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(pos, rot, boxCollider.size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}