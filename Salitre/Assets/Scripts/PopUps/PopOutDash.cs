using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutDash : PopUpCore
{
    protected override void OnAwake()
    {
        onAwake = PopUpSpawner.onAwakeStatic[1];
        onCompleted = PopUpSpawner.onCompletedStatic[1];

        onAwake.Invoke();
    }
    protected override void PoppingOutCondition()
    {
        if (input.GetButtonDown("Dash"))
        {
            popingOut = true;
        }
    }
}
