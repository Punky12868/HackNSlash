using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerPresets _playerData;
    [SerializeField] WeaponPreset _playerWeaponData;

    [SerializeField] PlayerInput playerSpeed;
    [SerializeField] Health playerHealth;
    [SerializeField] Weapon playerDamage;
    private void Start()
    {
        playerSpeed.speed = _playerData.speed;
        playerHealth.health = _playerData.health;
        playerDamage.damage = _playerWeaponData.damage;
    }
}
