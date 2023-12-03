using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFlashbackEnd : MonoBehaviour
{
    public bool mainMenu;
    int i;
    bool stop;
    private void Awake()
    {
        if (mainMenu)
        {
            i = FindObjectOfType<LevelLoader>().flashbackMenu;
        }
        else
        {
            i = FindObjectOfType<AllRooms>().currentDoor.levelID;
        }
    }
    private void Update()
    {
        if (!mainMenu)
        {
            if (i != FindObjectOfType<AllRooms>().currentDoor.levelID && !stop)
            {
                i = FindObjectOfType<AllRooms>().currentDoor.levelID;
            }
            else if (i == FindObjectOfType<AllRooms>().currentDoor.levelID && !stop)
            {
                stop = true;
            }
        }
        else
        {
            if (i != FindObjectOfType<LevelLoader>().flashbackMenu && !stop)
            {
                i = FindObjectOfType<LevelLoader>().flashbackMenu;
            }
            else if (i == FindObjectOfType<LevelLoader>().flashbackMenu && !stop)
            {
                stop = true;
            }
        }
    }
    public void NextLevel()
    {
        FindObjectOfType<LevelLoader>().LoadLevel(i);
    }
    public void DestroyFlashback()
    {
        Destroy(gameObject);
    }
}
