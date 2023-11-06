using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class WeaponCombo : MonoBehaviour
{
    Player player;

    public float attackRange = 0.5f;
    public int attackDamage;
    public int storedAttackDamage;

    public float attackRate = 2f;

    public bool canAttack;
    public static bool canMove;
    public static bool canAim;

    public Animator anim;
    public int combo;
    public float forceMagnitude;
    public Transform knockbackOrientation;

    bool chargeAttack;
    private void Start()
    {
        player = ReInput.players.GetPlayer(0);

        chargeAttack = true;
        canAttack = true;
        canMove = true;
        canAim = true;

        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (canAttack)
        {
            if (player.GetButtonTimedPress("Attack", 0) && chargeAttack)
            {
                Debug.Log("Charging");
            }
            else if (player.GetButtonUp("Attack") && chargeAttack)
            {
                chargeAttack = false;
                canMove = false;
                canAim = false;
                canAttack = false;
                anim.SetTrigger("" + combo);
                //GetComponent<SpawnHitbox>().Spawn(combo);
            }
            else if (player.GetButtonDown("Attack"))
            {
                chargeAttack = false;
                canMove = false;
                canAim = false;
                canAttack = false;
                anim.SetTrigger("" + combo);
                //GetComponent<SpawnHitbox>().Spawn(combo);
            }
        }
    }
    public void StartCombo(int i)
    {
        FindObjectOfType<SpawnHitbox>().Destroy();

        attackDamage += i;
        if (combo < 3)
        {
            combo++;
            canAttack = true;
        }
    }
    public void FinishCombo()
    {
        combo = 0;
        attackDamage = storedAttackDamage;
        canAttack = true;
        canMove = true;
        canAim = true;
        chargeAttack = true;
    }
    public void MoveForward()
    {
        FindObjectOfType<PlayerController>().MoveOnAttack(knockbackOrientation, forceMagnitude);
        /*Rigidbody playerRb = FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody>();
        playerRb.AddForce(orientation.forward * forceMagnitude, ForceMode.Impulse);*/
    }
}
