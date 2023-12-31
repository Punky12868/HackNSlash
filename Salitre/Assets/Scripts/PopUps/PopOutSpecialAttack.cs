using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutSpecialAttack : PopUpCore
{
    UnityEngine.UI.Slider powerSlider;
    bool canSpecial;
    protected override void OnAwake()
    {
        onAwake = PopUpSpawner.onAwakeStatic[3];
        onCompleted = PopUpSpawner.onCompletedStatic[3];

        onAwake.Invoke();

        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<UnityEngine.UI.Slider>();
    }
    protected override void PoppingOutCondition()
    {
        if (powerSlider.value >= powerSlider.maxValue && !canSpecial)
        {
            canSpecial = true;
        }
        else if (powerSlider.value < powerSlider.maxValue && !canSpecial)
        {
            powerSlider.value = powerSlider.maxValue;
        }

        if (input.GetButton("PowerAttack") && canSpecial)
        {
            popingOut = true;
        }
        
    }
}
