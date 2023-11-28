using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Rewired;

public class SelectWeapon : MonoBehaviour
{
    Player input;

    [SerializeField] Transform pickaxe, knife, bomb;
    public static Vector3 pickaxePos, knifePos, bombPos;
    public static int i;

    public UnityEvent selectRight;
    public UnityEvent selectLeft;

    private void Awake()
    {
        input = ReInput.players.GetPlayer(0);

        pickaxePos = pickaxe.position;
        knifePos = knife.position;
        bombPos = bomb.position;
    }
    private void Update()
    {
        if (input.GetButtonDown("WeaponSelectRight"))
        {
            selectRight.Invoke();
        }

        if (input.GetButtonDown("WeaponSelectLeft"))
        {
            selectLeft.Invoke();
        }
    }
}
