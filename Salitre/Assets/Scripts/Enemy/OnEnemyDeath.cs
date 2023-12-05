using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.AI;

public class OnEnemyDeath : MonoBehaviour
{
    public NpcId npcId;
    public int ID;
    UnityEngine.AI.NavMeshAgent agent;

    float speed, angularSpeed, acceleration;

    Rooms room;
    int i;
    [HideInInspector] public bool dead;
    private void Awake()
    {
        /*agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        speed = agent.speed;
        angularSpeed = agent.angularSpeed;
        acceleration = agent.acceleration;*/

        room = GetComponentInParent<Rooms>();
    }
    public void Death()
    {
        if (npcId != null)
        {
            FindObjectOfType<KarmaController>().KarmaIncreases();
        }

        if (i == 0)
        {
            i++;
            room.i--;

            room.CheckOpenDoor();
            //dead = true;
        }
    }
}
