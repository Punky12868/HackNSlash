using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFeedback : MonoBehaviour
{
    private void Awake()
    {
        GetComponentInParent<SelectedBehaviour>().arrow = this.gameObject;
        gameObject.SetActive(false);
    }
}
