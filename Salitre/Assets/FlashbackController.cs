using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbackController : MonoBehaviour
{
    public void Flashback()
    {
        if (SpawnFade.flashback)
        {
            FindObjectOfType<LevelLoader>().LoadFlashback();
        }
        else if (SpawnFade.nextLevelNoFlashback && !SpawnFade.flashback)
        {
            FindObjectOfType<LevelLoader>().LoadLevel(FindObjectOfType<AllRooms>().currentDoor.levelID);
        }
    }
}
