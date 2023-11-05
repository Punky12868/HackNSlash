using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHitbox : MonoBehaviour
{
    [HideInInspector] public GameObject currentHitbox;
    public Transform orientation;
    [SerializeField] GameObject[] hitboxType;
    public void Spawn(int i)
    {
        int motorIndex = 0; // the first motor
        float motorLevel = 1.0f; // full motor speed
        float duration = 0.3f; // .3 seconds

        FindObjectOfType<PlayerController>().player.SetVibration(motorIndex, motorLevel, duration);

        Instantiate(hitboxType[i], orientation);
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
