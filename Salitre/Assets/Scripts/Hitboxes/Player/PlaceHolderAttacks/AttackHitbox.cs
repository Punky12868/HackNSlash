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

        if (GetComponentInParent<DotToRandomTransform>())
        {
            GetComponentInParent<DotToRandomTransform>().GetComponentInChildren<SpawnHitbox>().currentHitbox = this.gameObject;

            pos = GetComponentInParent<DotToRandomTransform>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.position;
            rot = GetComponentInParent<DotToRandomTransform>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.rotation;
        }
        else
        {
            GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().currentHitbox = this.gameObject;

            pos = GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.position;
            rot = GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.rotation;
        }

        Collider[] hitEntities = Physics.OverlapBox(pos, boxCollider.size, rot, Hittable);

        foreach (Collider entity in hitEntities)
        {
            if (GetComponentInParent<DotToRandomTransform>())
            {
                Transform weaponDir = GetComponentInParent<DotToRandomTransform>().GetComponentInChildren<WeaponCombo>().knockbackOrientation;
                int damage = GetComponentInParent<DotToRandomTransform>().GetComponentInChildren<Weapon>().damage;

                entity.gameObject.GetComponent<TakeDamage>().GetDamage(damage, weaponDir);
                Debug.Log("AAAAAA");
            }

            if (GetComponentInParent<AimRotation>())
            {
                Transform weaponDir = GetComponentInParent<AimRotation>().aimOrientation;
                int damage = GetComponentInParent<AimRotation>().GetComponentInChildren<Weapon>().damage;

                entity.gameObject.GetComponent<TakeDamage>().GetDamage(damage, weaponDir);
                Debug.Log("BBBBBB");
            }

            Debug.Log("Hit: " + entity.gameObject.name);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(pos, rot, boxCollider.size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}