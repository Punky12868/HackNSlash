using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected override void OnEnter()
    {
        playerFollow.Weight = 0.5f;
        playerAvoidFollow.Weight = 1;
        patrolFollow.Weight = 0;
        movement.Speed = 0;

        if (sc.cr_AttackRunning || sc.cr_AvoidAttackRunning)
        {
            sc.StopCoroutinesCustom();
        }
    }
    protected override void OnUpdate()
    {
        if (!sc.cr_HurtRunning)
        {
            sc.Change(2);
        }
        else
        {
            sc.Change(3);
        }
    }
}
