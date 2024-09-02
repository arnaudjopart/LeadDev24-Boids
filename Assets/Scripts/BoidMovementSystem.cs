using System;
using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;
using static UnityEditor.Progress;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct BoidMovementSystem : ISystem
{
    void OnCreate(ref SystemState state) 
    {
    
    
    }
    void OnUpdate(ref SystemState state)
    {
        foreach (var item in SystemAPI.Query<BoidAspect>())//.WithAll<DemoTag>())
        {

            float3 separationDirection = GetSeparationDirection(item);
            float3 alignmentDirection = GetAlignmentDirection(item);
            float3 cohesionDirection = GetCohesionDirection(item); 
            var direction = math.normalize(separationDirection + alignmentDirection+ cohesionDirection);
            item.m_transform.ValueRW.Rotation = quaternion.LookRotation(math.normalizesafe(direction), new float3(0, 1, 0));


        }

    }

    private float3 GetCohesionDirection(BoidAspect item)
    {
        var neighbours = item.m_neighbors;
        if (neighbours.Length <= 0) return float3.zero;
        var positionToMoveToward = float3.zero;
        for (var i = 0; i < neighbours.Length; i++)
        {

            float3 vector = neighbours[i].Position;
            positionToMoveToward += vector;
        }

        positionToMoveToward /= neighbours.Length;
        var direction = math.normalize(positionToMoveToward - item.m_transform.ValueRO.Position);
        return direction*0.1f;
    }

    private float3 GetAlignmentDirection(BoidAspect item)
    {
        var neighbours = item.m_neighbors;
        if (neighbours.Length <= 0) return item.m_transform.ValueRO.Forward();
        var direction = float3.zero;
        for (var i = 0; i < neighbours.Length; i++)
        {

            float3 vector = (item.m_transform.ValueRO.Position - neighbours[i].Position);
            direction += math.normalize(vector) / math.distance(item.m_transform.ValueRO.Position, neighbours[i].Position);
        }

        return direction /= neighbours.Length;
    }

    private float3 GetSeparationDirection(BoidAspect item)
    {
        var neighbours = item.m_neighbors;
        if (neighbours.Length <= 0) return item.m_transform.ValueRO.Forward();
        var direction = float3.zero;
        for (var i = 0; i < neighbours.Length; i++)
        {

            float3 vector = neighbours[i].Forward;
            direction += vector;
        }

        return direction /= neighbours.Length;
    }
}
