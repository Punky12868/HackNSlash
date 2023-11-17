using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using EmeraldAI;

public class Rooms : MonoBehaviour
{
    EmeraldAISystem[] allEnemys;

    public bool activeRoom;
    public bool npcRoom;

    [SerializeField] GameObject[] allGo;

    [HideInInspector] public int enemysAlive;

    int i;
    private void Awake()
    {
        allEnemys = GetComponentsInChildren<EmeraldAISystem>();

        if (activeRoom)
        {
            ActivateRoom();
        }
        else
        {
            DeactivateRoom();
        }
    }
    private void Update()
    {
        if (enemysAlive > 0 && i == 0)
        {
            i++;

            DoorController[] allRoomDoors = GetComponentsInChildren<DoorController>();

            for (int i = 0; i < allRoomDoors.Length; i++)
            {
                allRoomDoors[i].canEnter = false;
            }
        }
        else if (enemysAlive <= 0 && i == 1)
        {
            i++;

            DoorController[] allRoomDoors = GetComponentsInChildren<DoorController>();

            for (int i = 0; i < allRoomDoors.Length; i++)
            {
                allRoomDoors[i].canEnter = true;
            }
        }
    }
    public void ActivateRoom()
    {
        activeRoom = true;

        for (int i = 0; i < allGo.Length; i++)
        {
            allGo[i].SetActive(true);
        }
    }
    public void DeactivateRoom()
    {
        activeRoom = false;

        for (int i = 0; i < allGo.Length; i++)
        {
            allGo[i].SetActive(false);
        }
    }
    public void ActivateDialog()
    {
        if (npcRoom)
        {
            GetComponentInChildren<DialogSystem>().ActivateDialog();
        }
    }
}
