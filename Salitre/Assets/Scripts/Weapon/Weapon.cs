using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bombb;
    [SerializeField] WeaponCombo normalWeapon;

    public int damage;

    public static int pickDamage;
    public static int knifeDamage;
    public static int bombDamage;

    public static IndividualPos pickaxe;
    public static IndividualPos knifea;
    public static IndividualPos bomb;

    MrBombastic mrBombastic;

    int i = 0;

    [SerializeField] Sprite[] selectDeselectSprites;
    private void Awake()
    {
        mrBombastic = FindObjectOfType<MrBombastic>();
        bombb.SetActive(false);
        mrBombastic.enabled = false;
    }
    private void Update()
    {
        if (pickaxe.i == i && damage != pickDamage)
        {
            if (!FindObjectOfType<PlayerInput>().enabled)
            {
                FindObjectOfType<PlayerInput>().enabled = true;
            }
            if (mrBombastic.isAiming)
            {
                mrBombastic.isAiming = false;
                mrBombastic.DestroyExplosionRadius();
            }
            bombb.SetActive(false);
            mrBombastic.enabled = false;
            normalWeapon.enabled = true;
            damage = pickDamage;

            pickaxe.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[0];
            knifea.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[4];
            bomb.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[5];
        }
        else if (knifea.i == i && damage != knifeDamage)
        {
            if (!FindObjectOfType<PlayerInput>().enabled)
            {
                FindObjectOfType<PlayerInput>().enabled = true;
            }
            if (mrBombastic.isAiming)
            {
                mrBombastic.isAiming = false;
                mrBombastic.DestroyExplosionRadius();
            }
            bombb.SetActive(false);
            mrBombastic.enabled = false;
            normalWeapon.enabled = true;
            damage = knifeDamage;

            pickaxe.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[3];
            knifea.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[1];
            bomb.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[5];
        }
        else if (bomb.i == i && damage != bombDamage)
        {
            bombb.SetActive(true);
            mrBombastic.enabled = true;
            normalWeapon.enabled = false;
            damage = bombDamage;

            pickaxe.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[3];
            knifea.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[4];
            bomb.GetComponent<UnityEngine.UI.Image>().sprite = selectDeselectSprites[2];
        }
    }
}
