using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedBehaviour : MonoBehaviour
{
    CustomButton customButton;
    [SerializeField] float scaleDamping = 3;
    [SerializeField] float scaleSelectedMultiplier;

    public GameObject arrow;
    private void Awake()
    {
        arrow.SetActive(false);
        customButton = GetComponent<CustomButton>();
    }
    private void Update()
    {
        if (customButton.selected && !arrow.activeInHierarchy)
        {
            arrow.SetActive(true);
        }
        else if (!customButton.selected && arrow.activeInHierarchy)
        {
            arrow.SetActive(false);
        }

        if (customButton.selected && transform.localScale.x != 1.4)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * scaleSelectedMultiplier, 1 - Mathf.Exp(-scaleDamping * Time.unscaledDeltaTime));
        }
        else if (!customButton.selected && transform.localScale.x != 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 1 - Mathf.Exp(-scaleDamping * Time.unscaledDeltaTime));
        }
    }
}
