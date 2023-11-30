using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovement : MonoBehaviour
{
    public float bombSpeed = 5f;

    private Vector3 throwDirection;

    public void ThrowBomb(Vector3 startPoint, Vector3 endPoint)
    {
        throwDirection = (endPoint - startPoint).normalized;
        StartCoroutine(MoveBomb());
    }

    IEnumerator MoveBomb()
    {
        while (true)
        {
            transform.Translate(throwDirection * bombSpeed * Time.deltaTime);

            yield return null;
        }
    }
}

