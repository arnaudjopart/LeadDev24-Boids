
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class DrawNearestNeighborsManager : SystemBase
{
    protected override void OnUpdate()
    {
        foreach (var (buffer, localTransform) in SystemAPI.Query<DynamicBuffer<NeighborsTransformBufferElement>,RefRO<LocalTransform>>())
        {
            foreach(var neighbour in buffer)
            {
                Debug.DrawLine(neighbour.Position, localTransform.ValueRO.Position);
            }
        }
    }

   
}
