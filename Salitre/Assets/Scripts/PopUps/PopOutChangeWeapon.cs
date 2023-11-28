using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutChangeWeapon : PopUpCore
{
    protected override void PoppingOutCondition()
    {
        if (input.GetButtonDown("WeaponSelectRight") || input.GetButtonDown("WeaponSelectLeft"))
        {
            popingOut = true;
        }
    }
}
