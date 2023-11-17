using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public bool activeRoom;
    public bool npcRoom;

    [SerializeField] GameObject[] allGo;
    private void Awake()
    {
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
}
