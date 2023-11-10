using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidState : State
{
    protected override void OnEnter()
    {
        playerFollow.Weight = 0;
        playerAvoidFollow.Weight = 1;
        patrolFollow.Weight = 0;

        sc.Change(1);
    }
}
