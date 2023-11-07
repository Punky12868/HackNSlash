using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [HideInInspector] public bool isFreezed;
    public IEnumerator StartFreeze(float i)
    {
        isFreezed = true;
        yield return new WaitForSeconds(i);
        isFreezed = false;
    }
}
