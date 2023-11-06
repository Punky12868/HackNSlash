using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class StaticInput : MonoBehaviour
{
    private int playerID = 0;
    public static Rewired.Player playerInput;
    private void Start()
    {
        ReInput.players.GetPlayer(playerID);
    }
}

