using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [HideInInspector] public GameObject currentDialogOnScreen;
    [SerializeField] Transform dialogSpawnPoint;
    [SerializeField] GameObject dialogPrefab;

    [TextArea] public string[] dialogs;

    bool dialogueActivated;
    public float clickCooldown;
    [HideInInspector] public float clickInternalCooldown;
    public UnityEvent OnDialogStart, OnDialogEnd;
    public void ActivateDialog()
    {
        if (!dialogueActivated)
        {
            dialogueActivated = true;
            Instantiate(dialogPrefab, dialogSpawnPoint);
            OnDialogStart.Invoke();
        }
    }
    private void Update()
    {
        if (dialogueActivated && clickInternalCooldown > 0)
        {
            clickInternalCooldown -= Time.deltaTime;
        }
    }
    public void DestroyCurrentDialog()
    {
        Destroy(currentDialogOnScreen);
    }
}
