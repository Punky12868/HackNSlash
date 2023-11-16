using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class AttackHitbox : MonoBehaviour
{
    public LayerMask Hittable;
    [SerializeField] BoxCollider boxCollider;
    Vector3 pos;
    Quaternion rot;
    public bool notEmerald;
    private void Awake()
    {

        boxCollider = GetComponent<BoxCollider>();

        if (!notEmerald)
        {
            GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().currentHitbox = this.gameObject;

            pos = GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.position;
            rot = GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint.rotation;

            Collider[] hitEmeraldEntities = Physics.OverlapBox(pos, boxCollider.size, rot, Hittable);

            foreach (Collider entity in hitEmeraldEntities)
            {
                if (entity.gameObject.GetComponent<EmeraldAISystem>() != null)
                {
                    int DamageAmount = GetComponentInParent<AimRotation>().GetComponentInChildren<Weapon>().damage;
                    entity.gameObject.GetComponent<EmeraldAISystem>().Damage(DamageAmount, EmeraldAISystem.TargetType.AI, transform, 300);
                }
                //Damages an AI's location based damage component
                else if (entity.gameObject.GetComponent<LocationBasedDamageArea>() != null)
                {
                    LocationBasedDamageArea LBDArea = entity.gameObject.GetComponent<LocationBasedDamageArea>();
                    int DamageAmount = GetComponentInParent<DotToRandomTransform>().GetComponentInChildren<Weapon>().damage;
                    LBDArea.DamageArea(DamageAmount, EmeraldAISystem.TargetType.AI, transform, 300);
                }

                Debug.Log("Hit: " + entity.gameObject.name);
            }
        }
        /*else
        {
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
        }*/
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(pos, rot, boxCollider.size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}