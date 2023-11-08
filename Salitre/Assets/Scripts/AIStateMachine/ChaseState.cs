using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    protected override void OnEnter()
    {
        playerFollow.Weight = 1;
        playerAvoidFollow.Weight = 0;
        patrolFollow.Weight = 0;
    }
    protected override void OnUpdate()
    {
        if (movement.Speed != sc.highSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, sc.highSpeed, 1 - Mathf.Exp(-sc.speedDamping * Time.unscaledDeltaTime));
        }
    }
}
