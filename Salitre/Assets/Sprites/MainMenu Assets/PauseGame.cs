using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PauseGame : MonoBehaviour
{
    Player input;

    public GameObject menuDisplay;
    public UnityEngine.UI.Button firstSelect;

    public static bool paused;
    private void Awake()
    {
        input = ReInput.players.GetPlayer(0);
        menuDisplay.SetActive(false);
    }
    private void Update()
    {
        if (input.GetButtonDown("Menu"))
        {
            PauseInput();
        }

        PauseOrUnpauseGame();
        ShowOrHideMenu();
    }
    public static void PauseInput()
    {
        if (paused)
        {
            paused = false;
        }
        else if (!paused)
        {
            paused = true;
        }
    }
    void PauseOrUnpauseGame()
    {
        
        if (paused && Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else if (!paused && Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        ShowOrHideMenu();
    }
    void ShowOrHideMenu()
    {
        if (paused && !menuDisplay.activeInHierarchy)
        {
            menuDisplay.SetActive(true);

            if (!GetCurrentInput.isMouseInput)
            {
                firstSelect.Select();
            }
        }
        else if (!paused && menuDisplay.activeInHierarchy)
        {
            menuDisplay.SetActive(false);
        }
    }
}
