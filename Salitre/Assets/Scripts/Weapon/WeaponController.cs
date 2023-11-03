using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Weapon weaponData;

    [HideInInspector] public int damage;
    private void Awake()
    {
        damage = weaponData.damage;
    }
}
