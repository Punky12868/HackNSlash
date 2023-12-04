using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;
using Rewired;

public class WeaponCombo : MonoBehaviour
{
    [SerializeField] Transform slashParent;
    [SerializeField] VisualEffect slash;

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

    [SerializeField] float attackCooldown;
    float internalAttackCooldown;
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
        if (internalAttackCooldown > 0)
        {
            internalAttackCooldown -= Time.deltaTime;
        }

        if (powerSlider.value == powerSlider.maxValue && !specialAttackOn)
        {
            specialAttackOn = true;
        }

        if (canAttack && internalAttackCooldown <= 0)
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

                    if (combo == 1)
                    {
                        slashParent.localRotation = Quaternion.Euler(slashParent.localRotation.x, slashParent.localRotation.y, 45);
                    }
                    else if (combo == 2)
                    {
                        slashParent.localRotation = Quaternion.Euler(slashParent.localRotation.x, slashParent.localRotation.y, -45);
                    }
                    else
                    {
                        slashParent.localRotation = Quaternion.Euler(slashParent.localRotation.x, slashParent.localRotation.y, 0);

                    }

                    slash.Play();
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
                    slash.Play();
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
        internalAttackCooldown = attackCooldown;
        combo = 0;
        //attackDamage = storedAttackDamage;
        canAttack = true;
        canMove = true;
        canAim = true;
    }
    public void CanMoveCanAim()
    {
        canMove = true;
        canAim = true;
    }
    #endregion

    public void MoveForward()
    {
        GetComponentInParent<Rigidbody>().AddForce(FindObjectOfType<AimRotation>().aimOrientation.forward.normalized * 7, ForceMode.Impulse);
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
                canAttack = true;
                canMove = true;
                canAim = true;
                antiBugTime = antiBugStoredTime;
            }
        }
        else if (canMove && antiBugTime < antiBugStoredTime)
        {
            antiBugTime = antiBugStoredTime;
        }
    }
}
