using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutZoom : PopUpCore
{
    protected override void OnAwake()
    {
        onAwake = PopUpSpawner.onAwakeStatic[5];
        onCompleted = PopUpSpawner.onCompletedStatic[5];

        onAwake.Invoke();
    }
    protected override void PoppingOutCondition()
    {
        if (input.GetAxis("Camera Zoom") != 0)
        {
            popingOut = true;
        }
    }
}
