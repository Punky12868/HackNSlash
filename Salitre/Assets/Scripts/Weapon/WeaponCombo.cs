using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class WeaponCombo : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    Player player;

    [HideInInspector] public bool canAttack;
    public static bool canMove;
    public static bool canAim;
    bool chargeAttack;

    [Header("Combo State")]

    public Animator anim;

    public int combo;

    public Transform knockbackOrientation;
    public float moveForwardForce;

    public bool isPlayer;
    public bool notPlayerAttack;

    [SerializeField] float antiBugTime;
    float antiBugStoredTime;

    private void Start()
    {
        player = ReInput.players.GetPlayer(0);

        antiBugStoredTime = antiBugTime;

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
            if (isPlayer)
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
            else
            {
                if (notPlayerAttack)
                {
                    anim.SetTrigger("" + combo);

                    notPlayerAttack = false;
                }
            }
        }
    }

    #region ComboLogic
    public void StartCombo(int i)
    {
        FindObjectOfType<SpawnHitbox>().Destroy();

        //attackDamage += i;
        if (combo < 3)
        {
            combo++;
            canAttack = true;
        }
    }
    public void FinishCombo()
    {
        combo = 0;
        //attackDamage = storedAttackDamage;
        canAttack = true;
        canMove = true;
        canAim = true;
        chargeAttack = true;
    }
    #endregion

    public void MoveForward()
    {
        GetComponentInParent<Knockback>().MoveOnAttack(knockbackOrientation, moveForwardForce);
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            if (antiBugTime > 0)
            {
                antiBugTime -= Time.fixedDeltaTime;
            }
            else
            {
                canMove = true;
                antiBugTime = antiBugStoredTime;
            }
        }
        else if (canMove && antiBugTime < antiBugStoredTime)
        {
            antiBugTime = antiBugStoredTime;
        }
    }
}
