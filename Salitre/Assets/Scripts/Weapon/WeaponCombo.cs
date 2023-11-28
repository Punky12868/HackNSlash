using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class WeaponCombo : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    Player player;

    public static bool canAttack;
    public static bool canMove;
    public static bool canAim;

    [Header("Combo State")]

    public Animator anim;

    public int combo;
    public int specialAttackID;

    public Transform knockbackOrientation;
    public float moveForwardForce;

    public bool isPlayer;
    public bool notPlayerAttack;

    [SerializeField] float antiBugTime;
    float antiBugStoredTime;

    UnityEngine.UI.Slider powerSlider;
    public static bool specialAttackOn;
    private void Start()
    {
        player = ReInput.players.GetPlayer(0);

        antiBugStoredTime = antiBugTime;

        canAttack = true;
        canMove = true;
        canAim = true;

        anim = GetComponent<Animator>();

        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<UnityEngine.UI.Slider>();
    }
    private void Update()
    {
        if (powerSlider.value == powerSlider.maxValue && !specialAttackOn)
        {
            specialAttackOn = true;
        }

        if (canAttack)
        {
            if (isPlayer && !PlayerInput.dashing)
            {
                /*if (player.GetButtonTimedPress("Attack", 0) && chargeAttack)
                {
                    Debug.Log("Charging");
                }*/
                if (player.GetButtonDown("Attack"))
                {
                    canMove = false;
                    canAim = false;
                    canAttack = false;
                    anim.SetTrigger("" + combo);
                    //GetComponent<SpawnHitbox>().Spawn(combo);
                }
                else if (player.GetButtonDown("PowerAttack") && specialAttackOn)
                {
                    specialAttackOn = true;
                    powerSlider.value = powerSlider.minValue;
                    canMove = false;
                    canAim = false;
                    canAttack = false;
                    anim.SetTrigger("" + specialAttackID);
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
    }
    #endregion

    public void MoveForward()
    {
        //GetComponentInParent<Knockback>().MoveOnAttack(knockbackOrientation, moveForwardForce);
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
