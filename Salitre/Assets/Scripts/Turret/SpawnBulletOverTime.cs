using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletOverTime : MonoBehaviour
{
    Rooms designedRoom;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] float fireRate;
    float internalFireRate;
    private void Awake()
    {
        internalFireRate = fireRate;

        designedRoom = GetComponentInParent<Rooms>();
    }
    private void Update()
    {
        if (designedRoom.activeRoom)
        {
            if (internalFireRate > 0)
            {
                internalFireRate -= Time.deltaTime;
            }
            else
            {
                internalFireRate = fireRate;
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }
}
