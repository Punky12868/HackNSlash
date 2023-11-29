using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutChangeWeapon : PopUpCore
{
    protected override void OnAwake()
    {
        onAwake = PopUpSpawner.onAwakeStatic[6];
        onCompleted = PopUpSpawner.onCompletedStatic[6];

        onAwake.Invoke();
    }
    protected override void PoppingOutCondition()
    {
        if (input.GetButtonDown("WeaponSelectRight") || input.GetButtonDown("WeaponSelectLeft"))
        {
            timeValue.value++;

            if (timeValue.value >= 3)
            {
                popingOut = true;
            }
        }
    }
}
