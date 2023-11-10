using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHitbox : MonoBehaviour
{
    [HideInInspector] public GameObject currentHitbox;
    public Transform hitboxSpawnPoint;
    [SerializeField] GameObject[] hitboxType;
    public void Spawn(int i)
    {
        if (GetComponent<WeaponCombo>().isPlayer)
        {
            int motorIndex = 0; // the first motor
            float motorLevel = 1.0f; // full motor speed
            float duration = 0.3f; // .3 seconds

            FindObjectOfType<PlayerInput>().player.SetVibration(motorIndex, motorLevel, duration);
        }

        Instantiate(hitboxType[i], hitboxSpawnPoint);
    }
    public void Destroy()
    {
        if (currentHitbox != null)
        {
            Destroy(currentHitbox.gameObject);
            currentHitbox = null;
        }
    }
}
