using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.PlanarMovement;
using Friedforfun.ContextSteering.Demo;

public class StateController : MonoBehaviour
{
    public float highSpeed;
    public float normalSpeed;
    public float lowSpeed;

    public float speedDamping;

    public PlanarMovement movement;
    public DotToLayer playerFollow;
    public DotToLayer playerAvoidFollow;
    public DotToRandomTransform patrolFollow;

    State currentState;

    public SleepState sleepState = new SleepState();
    public ChaseState chaseState = new ChaseState();
    public PatrolState patrolState = new PatrolState();
    public HurtState hurtState = new HurtState();
    private void Start()
    {
        movement.Speed = lowSpeed;

        ChangeState(patrolState);
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(sleepState);
            Debug.Log("Sleep State");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(chaseState);
            Debug.Log("Chase State");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(patrolState);
            Debug.Log("Patrol State");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeState(hurtState);
            Debug.Log("Hurt State");
        }
    }
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = newState;
        currentState.OnStateEnter(this);
    }
}
