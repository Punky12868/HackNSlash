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
