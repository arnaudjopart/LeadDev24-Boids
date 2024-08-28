using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BoidAuthoring : MonoBehaviour
{
    [SerializeField] private float m_detectionDistance;

    private class Baker : Baker<BoidAuthoring>
    {
        public override void Bake(BoidAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Boid
            {
                m_detectionDistance = authoring.m_detectionDistance
            }); ;
            AddBuffer<NeighborsTransformBufferElement>(entity);
        }
    }
}

public struct Boid : IComponentData
{
    public float m_detectionDistance;
}

public struct NeighborsTransformBufferElement : IBufferElementData
{
    public float3 Position;
    public float3 Forward;
}

public readonly partial struct BoidAspect : IAspect
{
    readonly RefRW<LocalTransform> m_transform;
    public readonly RefRW<Boid> m_boid;
    public readonly DynamicBuffer<NeighborsTransformBufferElement> m_neighbors;

    internal float3 GetPosition()
    {
        return m_transform.ValueRO.Position;
    }

    internal void Clear()
    {
        m_neighbors.Clear();
    }

    internal void AddToNeighborList(float3 position)
    {
        m_neighbors.Add(new NeighborsTransformBufferElement
        {
            Position = position
        });
    }
}