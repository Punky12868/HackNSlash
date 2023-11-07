using UnityEngine;
using Friedforfun.ContextSteering.Core;

namespace Friedforfun.ContextSteering.PlanarMovement
{
    public class DotToTransform : PlanarSteeringBehaviour
    {
        [SerializeField] public Transform[] Positions;

        protected override Vector3[] getPositionVectors()
        {
            return VectorsFromTransformArray.GetVectors(Positions);
        }

    }

}
