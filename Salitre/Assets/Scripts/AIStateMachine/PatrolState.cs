using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.PlanarMovement;

public class PatrolState : State
{
    protected override void OnEnter()
    {
        playerFollow.Weight = 0;
        playerAvoidFollow.Weight = 0;
        patrolFollow.Weight = 1;
    }
    protected override void OnUpdate()
    {
        if (movement.Speed != sc.lowSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, sc.lowSpeed, 1 - Mathf.Exp(-sc.speedDamping * Time.unscaledDeltaTime));
        }
    }
}
