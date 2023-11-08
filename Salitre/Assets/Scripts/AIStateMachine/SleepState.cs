using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : State
{
    protected override void OnEnter()
    {
        playerFollow.Weight = 0;
        playerAvoidFollow.Weight = 0;
        patrolFollow.Weight = 0;
    }
    protected override void OnUpdate()
    {
        if (movement.Speed != 0)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, 0, 1 - Mathf.Exp(-sc.speedDamping * Time.unscaledDeltaTime));
        }
    }
}
