using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using Rewired;
using TMPro;

public class DialoguePlayer : MonoBehaviour
{
    Player input;
    DialogSystem currentDialogSystem;
    [SerializeField] TypewriterByCharacter typewriter;
    [SerializeField] TMP_Text dialogueText;

    string[] dialogs;
    int dialogIndex;
    private void Awake()
    {
        dialogIndex = 0;
        input = ReInput.players.GetPlayer(0);

        currentDialogSystem = FindObjectOfType<AllRooms>().currentDoor.tpPoint.gameObject.GetComponentInParent<Rooms>().gameObject.GetComponentInChildren<DialogSystem>();
        dialogs = currentDialogSystem.dialogs;
        currentDialogSystem.currentDialogOnScreen = this.gameObject;

        currentDialogSystem.clickInternalCooldown = currentDialogSystem.clickCooldown;
    }
    private void Update()
    {
        if (dialogIndex == 0 && currentDialogSystem.clickInternalCooldown <= 0)
        {
            typewriter.ShowText(dialogs[dialogIndex]);
            typewriter.StartShowingText(true);
            dialogIndex++;
        }
        else if (input.GetButtonDown("NpcInteraction") && currentDialogSystem.clickInternalCooldown <= 0)
        {
            if (typewriter.isShowingText)
            {
                typewriter.SkipTypewriter();
            }
            else
            {
                if (dialogIndex != dialogs.Length)
                {
                    typewriter.ShowText(dialogs[dialogIndex]);
                    typewriter.StartShowingText(true);
                    dialogIndex++;
                }
                else
                {
                    currentDialogSystem.OnDialogEnd.Invoke();
                }
            }
            currentDialogSystem.clickInternalCooldown = currentDialogSystem.clickCooldown;
        }
    }
}
