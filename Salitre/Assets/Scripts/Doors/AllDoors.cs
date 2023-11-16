using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDoors : MonoBehaviour
{
    [HideInInspector] public DoorController currentDoor;
    DoorController[] doorControllers;
    private void Awake()
    {
        doorControllers = FindObjectsOfType<DoorController>();
    }
    public void StartTP()
    {
        if (currentDoor != null)
        {
            currentDoor.TpPlayer();
        }
    }
}
