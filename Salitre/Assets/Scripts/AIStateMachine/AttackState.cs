using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected override void OnEnter()
    {
        playerFollow.Weight = 1;
        playerAvoidFollow.Weight = 0;
        patrolFollow.Weight = 0;

        sc.startCombat = true;
        sc.Change(0);
    }
}
