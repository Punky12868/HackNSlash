using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomToggle : MonoBehaviour
{
    Toggle toggle;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
    public void SetToggle()
    {
        if (toggle.isOn)
        {
            toggle.isOn = false;
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            toggle.isOn = true;
            PlayerPrefs.SetInt("Tutorial", 0);
        }
    }
}
