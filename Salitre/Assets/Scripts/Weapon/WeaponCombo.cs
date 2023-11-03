using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class WeaponCombo : MonoBehaviour
{
    private int playerID = 0;
    [SerializeField] Rewired.Player player;

    public float attackRange = 0.5f;
    public int attackDamage;
    public int storedAttackDamage;

    public float attackRate = 2f;

    public bool canAttack;
    public static bool canAim;

    public Animator anim;
    public int combo;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerID);

        canAttack = true;
        canAim = true;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (canAttack)
        {
            if (player.GetButtonDown("Attack"))
            {
                canAim = false;
                canAttack = false;
                anim.SetTrigger("" + combo);
                Attack();
            }
        }
    }
    public void StartCombo(int i)
    {
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
        canAim = true;
    }
    private void Attack()
    {
        Debug.Log("Attack with combo N°" + combo);
        /*Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(fist.position, attackRange, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {

        }*/
    }
}
