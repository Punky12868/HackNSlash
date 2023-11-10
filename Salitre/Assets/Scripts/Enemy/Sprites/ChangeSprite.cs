using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.Demo;

public class ChangeSprite : MonoBehaviour
{
    public void SetSprite(string i)
    {
        switch (i)
        {
            case "Moving":

                GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[1];

                break;

            case "Resting":

                GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[0];

                break;

            case "Attacking":

                GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[1];

                break;

            default:
                break;
        }
    }
    public void FlipSprite(bool i)
    {
        GetComponent<SpriteRenderer>().flipX = i;
    }
}
