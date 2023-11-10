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
        if (movement.Speed != sc.normalSpeed && !sc.attackStatus)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, sc.normalSpeed, 1 - Mathf.Exp(-sc.speedDamping + 2 * Time.unscaledDeltaTime));
        }

        if (sc.startCombat && !sc.attackStatus)
        {
            if (Physics.CheckSphere(sc.transform.position, sc.attackStateRadius, sc.PlayerLayerMask))
            {
                sc.attackStatus = true;
                sc.ChangeState(sc.attackState);
                //Debug.Log("attack state on!");
            }
        }
    }
}
