using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class Rooms : MonoBehaviour
{
    EmeraldAISystem[] enemys;
    DoorController[] doors;

    public bool activeRoom;
    public bool npcRoom;

    [SerializeField] GameObject[] allGo;
    [HideInInspector] public int i;
    bool killedAllEnemies;
    private void Awake()
    {
        doors = GetComponentsInChildren<DoorController>();

        if (!npcRoom)
        {
            enemys = GetComponentsInChildren<EmeraldAISystem>();

            if (enemys.Length > 0)
            {
                i = enemys.Length;

                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].canEnter = false;
                }
            }
        }

        if (activeRoom)
        {
            ActivateRoom();
        }
        else
        {
            DeactivateRoom();
        }
    }
    public void ActivateRoom()
    {
        for (int i = 0; i < allGo.Length; i++)
        {
            allGo[i].SetActive(true);
        }
    }
    public void DeactivateRoom()
    {
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
    public void CheckOpenDoor()
    {
        if (i == 0 && !killedAllEnemies)
        {
            killedAllEnemies = true;

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].canEnter = true;
            }
        }
    }
}
