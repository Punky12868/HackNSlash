using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GetCurrentInput : MonoBehaviour
{
    Player input;
    public static bool isMouseInput = true;
    private void Awake()
    {
        input = ReInput.players.GetPlayer(0);
    }
    private void Update()
    {
        Controller activeController = input.controllers.GetLastActiveController();

        if (activeController != null)
        {
            if (activeController.type == ControllerType.Joystick)
            {
                isMouseInput = false;
            }
            else if (activeController.type == ControllerType.Keyboard || activeController.type == ControllerType.Mouse)
            {
                isMouseInput = true;
            }
        }
    }
}
