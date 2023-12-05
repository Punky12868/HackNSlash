using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using Rewired;
using TMPro;
using DG.Tweening;

public class DialoguePlayer : MonoBehaviour
{
    Player input;
    DialogSystem currentDialogSystem;
    [SerializeField] TypewriterByCharacter typewriter;
    [SerializeField] TMP_Text dialogueText;

    string[] dialogs;
    int dialogIndex;

    [SerializeField] Transform topBar, bottomBar, fadePanel, npcHead;
    [SerializeField] Transform topBarStartingPos, bottomBarStartingPos, npcHeadStartingPos;
    [SerializeField] Transform topBarEndingPos, bottomBarEndingPos, npcHeadEndingPos;

    [SerializeField] Sprite[] npcHeads;

    bool startAnimation;
    bool animationDone;
    private void Awake()
    {
        dialogIndex = 0;
        input = ReInput.players.GetPlayer(0);

        currentDialogSystem = FindObjectOfType<AllRooms>().currentDoor.tpPoint.gameObject.GetComponentInParent<Rooms>().gameObject.GetComponentInChildren<DialogSystem>();
        dialogs = currentDialogSystem.dialogs;
        currentDialogSystem.currentDialogOnScreen = this.gameObject;

        currentDialogSystem.clickInternalCooldown = currentDialogSystem.clickCooldown;

        npcHead.GetComponent<UnityEngine.UI.Image>().sprite = npcHeads[FindObjectOfType<NpcId>().ID];

        topBar.position = topBarStartingPos.position;
        bottomBar.position = bottomBarStartingPos.position;
        npcHead.position = npcHeadStartingPos.position;

        fadePanel.GetComponent<CanvasGroup>().alpha = 0;

        startAnimation = true;
    }
    private void Update()
    {
        if (bottomBar.position.y >= bottomBarEndingPos.position.y - 0.05)
        {
            animationDone = true;
        }
        else if (animationDone && bottomBar.position.y <= bottomBarStartingPos.position.y + 0.05)
        {
            currentDialogSystem.OnDialogEnd.Invoke();
        }

        if (animationDone)
        {
            DialogInteraction();
        }

        DialogAnimation();
    }
    void DialogInteraction()
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
                    startAnimation = false;
                }
            }
            currentDialogSystem.clickInternalCooldown = currentDialogSystem.clickCooldown;
        }
    }
    void DialogAnimation()
    {
        if (startAnimation)
        {
            topBar.DOMove(topBarEndingPos.position, 1f);
            bottomBar.DOMove(bottomBarEndingPos.position, 1f);
            npcHead.DOMove(npcHeadEndingPos.position, 1f);

            fadePanel.GetComponent<CanvasGroup>().DOFade(1f, 1f);
        }
        else
        {
            topBar.DOMove(topBarStartingPos.position, 1f);
            bottomBar.DOMove(bottomBarStartingPos.position, 1f);
            npcHead.DOMove(npcHeadStartingPos.position, 1f);

            fadePanel.GetComponent<CanvasGroup>().DOFade(0f, 1f);
        }
    }
}
