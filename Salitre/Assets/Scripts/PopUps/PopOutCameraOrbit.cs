using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutCameraOrbit : PopUpCore
{
    protected override void PoppingOutCondition()
    {
        if (input.GetButtonDown("Camera Orbit Right") || input.GetButtonDown("Camera Orbit Left"))
        {
            popingOut = true;
        }
    }
}
