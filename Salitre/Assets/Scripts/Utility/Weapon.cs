using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UrpiHat/Weapons/Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject skin;
    public Animator animator;

    public int damage;
}
