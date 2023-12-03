using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Transform parent;
    public void spawnOptions()
    {
        Instantiate(optionsMenu, parent);
        GetComponent<CustomButton>().selected = false;
    }
}
