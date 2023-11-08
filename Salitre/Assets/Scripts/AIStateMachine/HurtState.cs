using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected override void OnEnter()
    {
        playerFollow.Weight = 0;
        playerAvoidFollow.Weight = 1;
        patrolFollow.Weight = 0;
    }
    protected override void OnUpdate()
    {
        if (movement.Speed != sc.normalSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, sc.normalSpeed, 1 - Mathf.Exp(-sc.speedDamping * Time.unscaledDeltaTime));
        }
    }
}
