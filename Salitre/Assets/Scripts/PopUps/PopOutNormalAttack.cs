using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutNormalAttack : PopUpCore
{
    protected override void PoppingOutCondition()
    {
        if (input.GetButton("Attack"))
        {
            popingOut = true;
        }
    }
}
