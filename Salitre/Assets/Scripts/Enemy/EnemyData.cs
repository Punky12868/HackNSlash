using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] EnemyPreset _enemyData;
    [SerializeField] WeaponPreset _enemyWeaponData;

    [SerializeField] EnemyController enemySpeed;
    [SerializeField] Health enemyHealh;
    [SerializeField] Weapon enemyDamage;
    private void Start()
    {
        enemySpeed.speed = _enemyData.speed;
        enemyHealh.health = _enemyData.health;
        enemyDamage.damage = _enemyWeaponData.damage;
    }
}
