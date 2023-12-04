using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using EmeraldAI.Example;

public class HealthSlider : MonoBehaviour
{
    public Slider slider;
    EmeraldAIPlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = GetComponent<EmeraldAIPlayerHealth>();
        slider.value = playerHealth.CurrentHealth;
    }
    public void UpdateSlider()
    {
        slider.value = playerHealth.CurrentHealth;
    }
}
