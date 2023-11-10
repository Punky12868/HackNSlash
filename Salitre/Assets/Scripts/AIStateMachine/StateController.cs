using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.PlanarMovement;
using Friedforfun.ContextSteering.Demo;

public class StateController : MonoBehaviour
{
    public LayerMask PlayerLayerMask;
    public float chaseStateRadius;
    public float attackStateRadius;

    public float highSpeed;
    public float normalSpeed;
    public float lowSpeed;

    public float speedDamping;

    public PlanarMovement movement;
    public DotToLayer playerFollow;
    public DotToLayer playerAvoidFollow;
    public DotToRandomTransform patrolFollow;

    State currentState;

    public SleepState sleepState = new SleepState();
    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();
    public PatrolState patrolState = new PatrolState();
    public AvoidState avoidState = new AvoidState();
    public HurtState hurtState = new HurtState();

    public bool cr_AttackRunning;
    public bool cr_AvoidAttackRunning;
    public bool cr_HurtRunning;

    public bool attackStatus;
    public bool startCombat;

    public StateController[] allSelectedAgents;
    public bool doneCalling;
    private void Start()
    {
        movement.Speed = lowSpeed;

        ChangeState(patrolState);
    }
    void Update()
    {
        if (startCombat && !doneCalling)
        {
            for (int i = 0; i < allSelectedAgents.Length; i++)
            {
                if (!allSelectedAgents[i].startCombat && allSelectedAgents[i] != this)
                {
                    allSelectedAgents[i].doneCalling = true;
                    allSelectedAgents[i].OnCombat();
                }
            }
            doneCalling = true;
        }

        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }

        if (!startCombat)
        {
            if (Physics.CheckSphere(transform.position, chaseStateRadius, PlayerLayerMask) && currentState != chaseState && currentState != attackState)
            {
                ChangeState(chaseState);
                //Debug.Log("It hit the player!");
            }

            if (Physics.CheckSphere(transform.position, attackStateRadius, PlayerLayerMask) && currentState != attackState && currentState == chaseState)
            {
                ChangeState(attackState);
                //Debug.Log("attack state on!");
            }
        }
    }
    public void OnCombat()
    {
        ChangeState(chaseState);
        //Debug.Log(gameObject.name + " OnCombat");
    }
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = newState;
        currentState.OnStateEnter(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseStateRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackStateRadius);
    }
    public void Change(int i)
    {
        if (i == 0)
        {
            if (!cr_AttackRunning && !cr_AvoidAttackRunning)
            {
                StartCoroutine(AttackAnim());
            }
        }
        else if (i == 1)
        {
            if (!cr_AttackRunning && !cr_AvoidAttackRunning)
            {
                StartCoroutine(AvoidOnAttack());
            }
        }
        else if (i == 2)
        {
            StartCoroutine(AvoidPlayer());
        }
        else if (i == 3)
        {
            StopCoroutine(AvoidPlayer());

            cr_HurtRunning = false;

            StartCoroutine(AvoidPlayer());
        }
    }
    public void StopCoroutinesCustom()
    {
        if (cr_AttackRunning)
        {
            StopCoroutine(AttackAnim());
            cr_AttackRunning = false;
        }

        if (cr_AvoidAttackRunning)
        {
            StopCoroutine(AvoidOnAttack());
            cr_AvoidAttackRunning = false;
        }

        if (!cr_AvoidAttackRunning && !cr_AttackRunning)
        {
            StopAllCoroutines();
        }
    }
    public IEnumerator StartChasingAgain()
    {
        yield return new WaitForSeconds(1);
        //GetComponentInChildren<TrailRenderer>().enabled = false;
        ChangeState(avoidState);
    }
    public IEnumerator AttackAnim()
    {
        cr_AttackRunning = true;

        if (movement.Speed != lowSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, lowSpeed, 1 - Mathf.Exp(-speedDamping + 1 * Time.unscaledDeltaTime));
        }

        playerFollow.Weight = 0;
        playerAvoidFollow.Weight = 1;

        yield return new WaitForSeconds(0.5f);

        playerFollow.Weight = 1;
        playerAvoidFollow.Weight = 0;

        if (movement.Speed != highSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, highSpeed, 1 - Mathf.Exp(-speedDamping + 1 * Time.unscaledDeltaTime));
        }

        yield return new WaitForSeconds(0.3f);

        //Debug.Log("Before Attacking Anim");
        if (movement.Speed != 0)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, 0, 1 - Mathf.Exp(-speedDamping * Time.unscaledDeltaTime));
        }

        yield return new WaitForSeconds(1f);

        if (movement.Speed != highSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, highSpeed, 1 - Mathf.Exp(-speedDamping * Time.unscaledDeltaTime));
        }

        yield return new WaitForSeconds(0.2f);

        //Debug.Log("ATTACK!!");
        //GetComponentInChildren<TrailRenderer>().enabled = true;
        this.GetComponentInChildren<WeaponCombo>().notPlayerAttack = true;

        cr_AttackRunning = false;
        StartCoroutine(StartChasingAgain());
        ChangeState(sleepState);
    }
    public IEnumerator AvoidOnAttack()
    {
        cr_AvoidAttackRunning = true;

        playerFollow.Weight = 0;
        playerAvoidFollow.Weight = 1;

        if (movement.Speed != highSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, highSpeed, 1 - Mathf.Exp(-speedDamping * Time.unscaledDeltaTime));
        }

        yield return new WaitForSeconds(0.5f);

        if (movement.Speed != lowSpeed)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, lowSpeed, 1 - Mathf.Exp(-speedDamping * Time.unscaledDeltaTime));
        }

        yield return new WaitForSeconds(1f);

        cr_AvoidAttackRunning = false;
        StartCoroutine(Waiting());
        ChangeState(chaseState);
    }
    public IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.5f);
        attackStatus = false;
    }
    public IEnumerator AvoidPlayer()
    {
        cr_HurtRunning = true;

        yield return new WaitForSeconds(0.2f);

        if (movement.Speed != normalSpeed && !attackStatus)
        {
            movement.Speed = Mathf.Lerp(movement.Speed, normalSpeed, 1 - Mathf.Exp(-speedDamping + 2 * Time.unscaledDeltaTime));
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Waiting());
        ChangeState(chaseState);

        cr_HurtRunning = false;
    }
}
