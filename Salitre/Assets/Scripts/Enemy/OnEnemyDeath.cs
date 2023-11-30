using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyDeath : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    float speed, angularSpeed, acceleration;

    Rooms room;
    int i;
    [HideInInspector] public bool dead;
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        speed = agent.speed;
        angularSpeed = agent.angularSpeed;
        acceleration = agent.acceleration;

        room = GetComponentInParent<Rooms>();
    }
    public void Death()
    {
        if (i == 0)
        {
            i++;
            room.i--;

            room.CheckOpenDoor();
            //dead = true;
        }
    }
    public IEnumerator OnHit()
    {
        /*agent.speed = 10;
        agent.angularSpeed = 0;
        agent.acceleration = 20;*/

        yield return new WaitForSeconds(.5f);

        /*agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;*/
    }
}
