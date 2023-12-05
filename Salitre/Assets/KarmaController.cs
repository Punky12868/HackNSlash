using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaController : MonoBehaviour
{
    public GameObject[] karmaKnobs;
    public int karma;
    public void KarmaIncreases()
    {
        karma++;
        CheckKarmaLevel();
    }

    void CheckKarmaLevel()
    {
        if (karma == 1)
        {
            karmaKnobs[0].SetActive(true);
        }
        else if (karma == 2)
        {
            karmaKnobs[1].SetActive(true);
        }
        else if (karma == 3)
        {
            karmaKnobs[2].SetActive(true);
        }
    }
}
