using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.Integration.UnityUI;

public class DestroyMenu : MonoBehaviour
{
    RewiredEventSystem rewiredEventSystem;
    Player input;
    public GameObject menu;
    GameObject button;
    private void Awake()
    {
        rewiredEventSystem = FindObjectOfType<RewiredEventSystem>();
        button = FindObjectOfType<spawnOptionsMenu>().gameObject;

        input = ReInput.players.GetPlayer(0);
    }
    private void Update()
    {
        if (input.GetButtonDown("Volver"))
        {
            DestroyMenuObject();
        }
    }
    public void DestroyMenuObject()
    {
        button.GetComponent<CustomButton>().Select();
        rewiredEventSystem.SetSelectedGameObject(button);
        Destroy(menu);
    }
}
