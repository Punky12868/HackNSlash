using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    private void Update()
    {
        if (!WeaponCombo.canMove)
        {
            GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[2];
            FindObjectOfType<TrailRenderer>().enabled = true;
        }
        else
        {
            FindObjectOfType<TrailRenderer>().enabled = false;
            if (playerInput.moveDir.x > 0.1)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[1];
            }
            else if (playerInput.moveDir.x < -0.1)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[1];
            }
            else if (playerInput.moveDir.y > 0.1 || playerInput.moveDir.y < -0.1)
            {
                GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[1];
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = GetComponent<AllSprites>().sprites[0];
            }
        }
    }
}
