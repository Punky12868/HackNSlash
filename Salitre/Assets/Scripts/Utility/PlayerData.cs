using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UrpiHat/Player")]
public class PlayerData : ScriptableObject
{
    public Mesh skin;
    public Animator animator;

    public float health;
    public float speed;
}
