using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFlashbackEnd : MonoBehaviour
{
    int i;
    private void Awake()
    {
        i = FindObjectOfType<AllRooms>().currentDoor.levelID;
    }
    public void NextLevel()
    {
        FindObjectOfType<LevelLoader>().LoadLevel(i);
    }
}
