using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutCameraOrbit : PopUpCore
{
    protected override void OnAwake()
    {
        onAwake = PopUpSpawner.onAwakeStatic[4];
        onCompleted = PopUpSpawner.onCompletedStatic[4];

        onAwake.Invoke();
    }
    protected override void PoppingOutCondition()
    {
        if (input.GetButtonDown("Camera Orbit Right") || input.GetButtonDown("Camera Orbit Left"))
        {
            popingOut = true;
        }
    }
}
