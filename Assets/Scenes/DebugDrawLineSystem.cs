using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class DebugDrawLineSystem : SystemBase
{
    protected override void OnUpdate()
    {
        foreach (var (transform,buffer) in SystemAPI.Query<RefRO<LocalTransform>,DynamicBuffer<NeighborsTransformBufferElement>>())
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                Debug.DrawLine(transform.ValueRO.Position, buffer[i].Position);

            }
        }
        
    }

}
