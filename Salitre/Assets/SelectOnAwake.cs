using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOnAwake : MonoBehaviour
{
    public CustomButton juegoButton;
    public CustomSlider customSlider;
    private void Awake()
    {
        customSlider.Select();
    }
}
