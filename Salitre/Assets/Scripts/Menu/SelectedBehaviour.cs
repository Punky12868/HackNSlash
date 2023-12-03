using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedBehaviour : MonoBehaviour
{
    CustomButton customButton;
    [SerializeField] CustomSlider customSlider;
    [SerializeField] float scaleDamping = 20;
    [SerializeField] float scaleSelectedMultiplier = 1.4f;

    public GameObject arrow;
    private void Awake()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
        }

        customButton = GetComponent<CustomButton>();
    }
    private void Update()
    {
        if (arrow != null && customButton != null)
        {
            if (customButton.selected && !arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
            }
            else if (!customButton.selected && arrow.activeInHierarchy)
            {
                arrow.SetActive(false);
            }
        }
        else if (arrow != null && customSlider != null)
        {
            if (customSlider.selected && !arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
            }
            else if (!customSlider.selected && arrow.activeInHierarchy)
            {
                arrow.SetActive(false);
            }
        }

        if (customButton != null)
        {
            if (customButton.selected && transform.localScale.x != 1.4)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * scaleSelectedMultiplier, 1 - Mathf.Exp(-scaleDamping * Time.unscaledDeltaTime));
            }
            else if (!customButton.selected && transform.localScale.x != 1)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 1 - Mathf.Exp(-scaleDamping * Time.unscaledDeltaTime));
            }
        }
        if (customSlider != null)
        {
            if (customSlider.selected && transform.localScale.x != 1.4)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * scaleSelectedMultiplier, 1 - Mathf.Exp(-scaleDamping * Time.unscaledDeltaTime));
            }
            else if (!customSlider.selected && transform.localScale.x != 1)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 1 - Mathf.Exp(-scaleDamping * Time.unscaledDeltaTime));
            }
        }
    }
}
