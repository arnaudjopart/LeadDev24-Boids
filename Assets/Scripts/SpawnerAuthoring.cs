
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpawnerAuthoring : MonoBehaviour
{
    [SerializeField] float m_spawnFrequency;
    [SerializeField] uint m_seed;

    private class Baker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var thisEntity = GetEntity(TransformUsageFlags.WorldSpace);
            AddComponent(thisEntity, new SpawnerComponentData
            {
                SpawnFrequency = authoring.m_spawnFrequency,
                m_random = Random.CreateFromIndex(authoring.m_seed)
            });
            AddBuffer<SpawnPositionElement>(thisEntity);
            AddComponent(thisEntity,new TimerComponentData { m_time = 0 });

        }
    }
}

public struct SpawnerComponentData : IComponentData
{
    public float SpawnFrequency;
    public Random m_random;
}


public struct SpawnPositionElement : IBufferElementData
{
    public float3 Value;
}
