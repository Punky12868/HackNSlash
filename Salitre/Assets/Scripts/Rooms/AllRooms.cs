using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AllRooms : MonoBehaviour
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
    public void DeactivateLastRoom()
    {
        currentDoor.gameObject.GetComponentInParent<Rooms>().DeactivateRoom();
    }
    public void GetNpcDialog()
    {
        currentDoor.CheckIfNpcRoom();
    }
}
