using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.PlanarMovement;

public class StateController : MonoBehaviour
{
    [SerializeField] DotToLayer playerFollow;
    [SerializeField] DotToLayer playerAvoidFollow;
    [SerializeField] DotToRandomTransform patrolFollow;

    State currentState;
    public SleepState sleepState = new SleepState();
    public ChaseState chaseState = new ChaseState();
    public PatrolState patrolState = new PatrolState();
    public HurtState hurtState = new HurtState();
    private void Start()
    {
        ChangeState(patrolState);
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
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
