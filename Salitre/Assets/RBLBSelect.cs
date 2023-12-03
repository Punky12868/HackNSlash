using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.UI;
using UnityEngine.EventSystems;
using Rewired.Integration.UnityUI;

public class RBLBSelect : MonoBehaviour
{
    public Player input;
    public RewiredEventSystem rewiredEventSystem;

    [SerializeField] CustomSlider juegoFirstSelected;
    [SerializeField] CustomSlider audioFirstSelected;

    [SerializeField] GameObject juegoPanel;
    [SerializeField] GameObject audioPanel;

    [SerializeField] GameObject[] arrowsJuego;
    [SerializeField] GameObject[] arrowsAudio;

    int selectIndex;
    private void Awake()
    {
        input = ReInput.players.GetPlayer(0);
        rewiredEventSystem = FindObjectOfType<RewiredEventSystem>();
        Select();
    }
    private void Update()
    {
        if (input.GetButtonDown("RB"))
        {
            selectIndex++;
            Select();
        }

        if (input.GetButtonDown("LB"))
        {
            selectIndex--;
            Select();
        }
    }
    public void Select()
    {
        if (selectIndex > 1)
        {
            selectIndex = 0;
        }
        else if (selectIndex < 0)
        {
            selectIndex = 1;
        }

        if (selectIndex == 0)
        {
            SelectJuego();
        }
        else
        {
            SelectAudio();
        }
    }
    public void SelectJuego()
    {
        audioFirstSelected.selected = false;

        audioPanel.SetActive(false);
        juegoPanel.SetActive(true);

        rewiredEventSystem.SetSelectedGameObject(juegoFirstSelected.gameObject);
        juegoFirstSelected.Select();

        for (int i = 0; i < arrowsAudio.Length; i++)
        {
            arrowsAudio[i].SetActive(false);
        }
    }
    public void SelectAudio()
    {
        juegoFirstSelected.selected = false;

        audioPanel.SetActive(true);
        juegoPanel.SetActive(false);

        rewiredEventSystem.SetSelectedGameObject(audioFirstSelected.gameObject);
        audioFirstSelected.Select();

        for (int i = 0; i < arrowsJuego.Length; i++)
        {
            arrowsJuego[i].SetActive(false);
        }
    }
}
