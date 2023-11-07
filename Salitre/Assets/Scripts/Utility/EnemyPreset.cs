using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UrpiHat/Entities/Enemy")]
public class EnemyPreset : ScriptableObject
{
    public Mesh skin;
    public Animator animator;

    public GameObject drop;

    public int health;
    public float speed;
}
