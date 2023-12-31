using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomButton : Button
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
    public void changeColor(string color)
    {
        ColorBlock colors = new ColorBlock();
        colors.normalColor = ColorUtility.TryParseHtmlString(color, out Color newColor) ? newColor : colors.normalColor;
        this.colors = colors;
    }
}
