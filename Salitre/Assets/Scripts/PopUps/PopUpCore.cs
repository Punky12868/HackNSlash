using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using Rewired;

public class PopUpCore : MonoBehaviour
{
    public Player input;

    [HideInInspector] public Vector2 startPos, endPos;
    public float travelTime = 1;

    [SerializeField] float popOutTime = 1;
    float destroyTime = 1;
    [HideInInspector] public bool popingOut;
    bool destroying;
    bool onCompletedEventInvoked;

    [SerializeField] bool usingSlider;
    [SerializeField] float timeBeforeTakingInput = 1.5f;
    [HideInInspector] public UnityEngine.UI.Slider timeValue;

    public UnityEvent onAwake, onCompleted;
    private void Awake()
    {
        input = ReInput.players.GetPlayer(0);

        if (usingSlider)
        {
            timeValue = GetComponentInChildren<UnityEngine.UI.Slider>();
        }

        if (PopUpSpawner.izqPopUp)
        {
            startPos = PopUpSpawner.izqStaticStartPos.position;
            endPos = PopUpSpawner.izqStaticEndPos.position;
        }
        else
        {
            startPos = PopUpSpawner.derStaticStartPos.position;
            endPos = PopUpSpawner.derStaticEndPos.position;
        }

        transform.position = startPos;
        transform.DOMove(endPos, travelTime).SetEase(Ease.InOutElastic);

        OnAwake();
    }
    private void Update()
    {
        if (timeBeforeTakingInput > 0)
        {
            timeBeforeTakingInput -= Time.deltaTime;
        }
        else
        {
            if (popingOut)
            {
                if (!onCompletedEventInvoked)
                {
                    onCompletedEventInvoked = true;
                    onCompleted.Invoke();
                }

                if (popOutTime > 0)
                {
                    popOutTime -= Time.deltaTime;
                }
                else if (popOutTime <= 0 && !destroying)
                {
                    transform.DOMove(startPos, travelTime).SetEase(Ease.InOutElastic);
                    destroying = true;
                }
            }
            else
            {
                PoppingOutCondition();
            }

            if (destroying)
            {
                if (destroyTime > 0)
                {
                    destroyTime -= Time.deltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    protected virtual void PoppingOutCondition()
    {
    }
    protected virtual void OnAwake()
    { 
    }
}
