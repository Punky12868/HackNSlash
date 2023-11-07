using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Friedforfun.ContextSteering.Core;
using Friedforfun.ContextSteering.PlanarMovement;

public class DotToRandomTransformMask : PlanarSteeringMask
{
    [SerializeField]
    public Transform[] Positions;
    protected override Vector3[] getPositionVectors()
    {
        return VectorsFromTransformArray.GetVectors(Positions);
    }
}
