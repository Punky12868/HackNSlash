using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Enemy enemyData;

    [HideInInspector] public float health;
    [HideInInspector] public float speed;
    private void Awake()
    {
        health = enemyData.health;
        speed = enemyData.speed;
    }
}
