using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutSpecialAttack : PopUpCore
{
    UnityEngine.UI.Slider powerSlider;

    protected override void OnAwake()
    {
        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<UnityEngine.UI.Slider>();
    }
    protected override void PoppingOutCondition()
    {
        if (powerSlider.value < powerSlider.maxValue)
        {
            powerSlider.value = powerSlider.maxValue;
        }

        if (input.GetButtonDown("PowerAttack") && powerSlider.value == powerSlider.maxValue)
        {
            popingOut = true;
        }
    }
}
