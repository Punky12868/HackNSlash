using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbackController : MonoBehaviour
{
    public void Flashback()
    {
        if (FindObjectOfType<SpawnFade>().flashback)
        {
            FindObjectOfType<LevelLoader>().LoadFlashback();
        }
        else if (FindObjectOfType<SpawnFade>().nextLevelNoFlashback && !FindObjectOfType<SpawnFade>().flashback)
        {
            FindObjectOfType<LevelLoader>().LoadLevel(FindObjectOfType<AllRooms>().currentDoor.levelID);
        }
    }
}
