using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyDeath : MonoBehaviour
{
    Rooms room;
    int i;
    private void Awake()
    {
        room = GetComponentInParent<Rooms>();
    }
    public void Death()
    {
        if (i == 0)
        {
            i++;
            room.i--;

            room.CheckOpenDoor();
        }
    }
}
