using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    [SerializeField] GameObject bombHitbox;
    AttackHitbox currentBombHitbox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(bombHitbox, transform.position, Quaternion.identity);
            currentBombHitbox = GameObject.FindGameObjectWithTag("BombHitbox").GetComponent<AttackHitbox>();
            currentBombHitbox.bombTransform = this.transform;
        }
    }
    private void Update()
    {
        if (currentBombHitbox != null && currentBombHitbox.bombTransform != null)
        {
            Destroy(gameObject);
        }
    }
}
