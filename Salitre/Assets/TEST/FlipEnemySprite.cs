using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlipEnemySprite : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private SpriteRenderer[] spriteRenderers;

    [Tooltip ("If the sprites are facing left activate this option to invert the flip")]
    [SerializeField] bool facingRight;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 velocity = navMeshAgent.velocity;

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            if (velocity.x > 0)
            {
                if (!facingRight)
                {
                    renderer.flipX = true;
                }
                else
                {
                    renderer.flipX = false;
                }
            }
            else if (velocity.x < 0)
            {
                if (!facingRight)
                {
                    renderer.flipX = false;
                }
                else
                {
                    renderer.flipX = true;
                }
            }
        }
    }
}
