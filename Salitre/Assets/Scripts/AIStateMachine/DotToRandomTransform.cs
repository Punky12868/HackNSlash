using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.Core;
using Friedforfun.ContextSteering.PlanarMovement;

public class DotToRandomTransform : PlanarSteeringBehaviour
{
    [Range(0, 50)]
    [SerializeField] float nextPos;

    [SerializeField] float returnTime;
    [SerializeField] float returnRadius;

    [SerializeField] float randomDirTime;

    Vector3 SpawnPoint;
    [SerializeField] Transform goTo;
    [SerializeField] Transform[] NextPoint;

    bool returning;
    bool cr_PatrolReturning;
    bool cr_NewNumberRunning;
    private void Awake()
    {
        SpawnPoint = transform.position;
        NextPoint[0] = goTo;

        goTo.name = "RandomDir for: " + gameObject.name;
        goTo.SetParent(GameObject.FindGameObjectWithTag("RandomDir").gameObject.transform);
    }
    private void Update()
    {

        if (transform.position.x > SpawnPoint.x + returnRadius || transform.position.x < SpawnPoint.x - returnRadius ||
            transform.position.y > SpawnPoint.y + returnRadius || transform.position.y < SpawnPoint.y - returnRadius)
        {
            if (!cr_PatrolReturning && !cr_NewNumberRunning)
            {
                StartCoroutine(PatrolAgain(returnTime));
            }
        }
    }
    protected override Vector3[] getPositionVectors()
    {
        if (!cr_NewNumberRunning && !cr_PatrolReturning)
        {
            StartCoroutine(GetRandomDir(randomDirTime));
        }

        return VectorsFromTransformArray.GetVectors(NextPoint);
    }
    IEnumerator PatrolAgain(float i)
    {
        cr_PatrolReturning = true;
        returning = true;

        NextPoint[0].position = SpawnPoint;

        yield return new WaitForSeconds(i);
        returning = false;
        cr_PatrolReturning = false;
    }
    IEnumerator GetRandomDir(float i)
    {
        cr_NewNumberRunning = true;

        if (!returning)
        {
            float randIntX = Random.Range(0, nextPos);
            float randIntZ = Random.Range(0, nextPos);

            Vector3 randomDir = new Vector3(randIntX, 0, randIntZ);
            NextPoint[0].position = SpawnPoint + randomDir;
        }

        yield return new WaitForSeconds(i);
        cr_NewNumberRunning = false;
    }
}
