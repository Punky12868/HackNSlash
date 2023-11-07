using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UrpiHat/Player")]
public class PlayerPresets : ScriptableObject
{
    public Mesh skin;
    public Animator animator;

    public int health;
    public float speed;
}
