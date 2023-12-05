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
    int i;

    [SerializeField] bool isBomb;
    [SerializeField] float lifeTime = 0.15f;
    [HideInInspector] public Transform bombTransform;

    private void Awake()
    {
        if (isBomb)
        {
            bombTransform = GameObject.FindGameObjectWithTag("Bomb").transform;
        }

        boxCollider = GetComponent<BoxCollider>();

        if (!notEmerald)
        {
            UnityEngine.UI.Slider powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<UnityEngine.UI.Slider>();

            // Assuming hitboxSpawnPoint is a transform, ensure it's not null

            Transform hitboxSpawnPoint;

            if (!isBomb)
            {
                hitboxSpawnPoint = GetComponentInParent<AimRotation>().GetComponentInChildren<SpawnHitbox>().hitboxSpawnPoint;

                pos = hitboxSpawnPoint.position;
                rot = hitboxSpawnPoint.rotation;
            }
            else
            {
                pos = bombTransform.position;
                rot = Quaternion.identity;
            }

            Collider[] hitEmeraldEntities = Physics.OverlapBox(pos, boxCollider.size, rot, Hittable);

            foreach (Collider entity in hitEmeraldEntities)
            {
                if (entity.gameObject.GetComponent<OnEnemyDeath>() != null)
                {
                    Debug.Log("uh");
                    entity.gameObject.GetComponent<OnEnemyDeath>().Death();
                    entity.gameObject.SetActive(false);
                }

                if (entity.gameObject.GetComponent<EmeraldAISystem>() != null)
                {

                    int DamageAmount = FindObjectOfType<Weapon>().damage;
                    Vector3 KnockDir = FindObjectOfType<AimRotation>().aimOrientation.forward;
                    //int DamageAmount = GetComponentInParent<AimRotation>().GetComponentInChildren<Weapon>().damage;
                    //Vector3 KnockDir = GetComponentInParent<AimRotation>().aimOrientation.forward;
                    entity.gameObject.GetComponent<EmeraldAISystem>().Damage(DamageAmount, EmeraldAISystem.TargetType.AI, transform, 300);

                    /*if (!entity.gameObject.GetComponent<OnEnemyDeath>().dead)
                    {
                        entity.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity = KnockDir.normalized * 15;
                        entity.gameObject.GetComponent<OnEnemyDeath>().StartCoroutine(entity.gameObject.GetComponent<OnEnemyDeath>().OnHit());
                    }*/
                    
                    if (powerSlider.value < powerSlider.maxValue && !WeaponCombo.specialAttackOn)
                    {
                        powerSlider.value++;
                    }
                }

                Debug.Log("Hit: " + entity.gameObject.name);
            }

            if (WeaponCombo.specialAttackOn)
            {
                WeaponCombo.specialAttackOn = false;
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(pos, rot, boxCollider.size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
    private void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}