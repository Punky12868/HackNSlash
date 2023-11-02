using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UrpiHat/Entities/Enemy")]
public class Enemy : ScriptableObject
{
    public Mesh skin;
    public Animator animator;

    public GameObject drop;

    public float health;
    public float speed;
}
