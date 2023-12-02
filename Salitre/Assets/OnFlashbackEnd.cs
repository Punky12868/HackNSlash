using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFlashbackEnd : MonoBehaviour
{
    int i;
    bool stop;
    private void Awake()
    {
        i = FindObjectOfType<AllRooms>().currentDoor.levelID;
        Debug.Log(i);
    }
    private void Update()
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
    public void NextLevel()
    {
        FindObjectOfType<LevelLoader>().LoadLevel(i);
    }
    public void DestroyFlashback()
    {
        Destroy(gameObject);
    }
}
