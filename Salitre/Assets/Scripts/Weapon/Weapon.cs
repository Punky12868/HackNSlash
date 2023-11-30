using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    public static int pickDamage;
    public static int knifeDamage;
    public static int bombDamage;

    public static IndividualPos pickaxe;
    public static IndividualPos knife;
    public static IndividualPos bomb;

    int i = 0;
    private void Update()
    {
        if (pickaxe.i == i && damage != pickDamage)
        {
            damage = pickDamage;
        }
        else if (knife.i == i && damage != knifeDamage)
        {
            damage = knifeDamage;
        }
        else if (bomb.i == i && damage != bombDamage)
        {
            damage = bombDamage;
        }
    }
}
