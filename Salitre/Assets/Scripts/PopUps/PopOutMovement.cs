using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopOutMovement : PopUpCore
{
    float movingTime;
    protected override void OnAwake()
    {
        onAwake = PopUpSpawner.onAwakeStatic[0];
        onCompleted = PopUpSpawner.onCompletedStatic[0];

        onAwake.Invoke();
    }
    protected override void PoppingOutCondition()
    {
        timeValue.value = movingTime;

        if (FindObjectOfType<PlayerInput>().moveDir != Vector3.zero)
        {
            movingTime += Time.deltaTime;
        }

        if (movingTime > 2)
        {
            popingOut = true;
        }
    }
}
