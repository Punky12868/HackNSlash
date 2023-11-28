using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutDash : PopUpCore
{
    protected override void PoppingOutCondition()
    {
        if (input.GetButtonDown("Dash"))
        {
            popingOut = true;
        }
    }
}
