using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlipEnemySprite : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [Tooltip ("If the sprites are facing left activate this option to invert the flip")]
    [SerializeField] bool facingRight;
    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 velocity = navMeshAgent.velocity;

        if (velocity.x > 0)
        {
            if (!facingRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (velocity.x < 0)
        {
            if (!facingRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
