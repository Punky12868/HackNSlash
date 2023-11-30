using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutNormalAttack : PopUpCore
{
    protected override void OnAwake()
    {
        onAwake = PopUpSpawner.onAwakeStatic[2];
        onCompleted = PopUpSpawner.onCompletedStatic[2];

        onAwake.Invoke();
    }
    protected override void PoppingOutCondition()
    {
        if (input.GetButton("Attack"))
        {
            popingOut = true;
        }
    }
}
