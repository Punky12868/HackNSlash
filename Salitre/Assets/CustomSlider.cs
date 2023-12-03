using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : Slider
{
    public bool selected;
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        switch (state)
        {
            case SelectionState.Normal:

                selected = false;

                break;
            case SelectionState.Highlighted:

                selected = true;

                break;
            case SelectionState.Selected:

                selected = true;

                break;
        }
    }
}
