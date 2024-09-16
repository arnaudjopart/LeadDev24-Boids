

using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public partial struct GetNearestEntitiesSystem : ISystem
{
    private EntityQuery m_query;

    public void OnCreate(ref SystemState state)
    {
        m_query = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<LocalTransform>()
            .WithAll<Boid>()
            .Build(ref state);
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var otherBoids = m_query.ToEntityArray(Allocator.Temp);

        foreach (var (aspect,entity) in SystemAPI.Query<BoidAspect>().WithEntityAccess())
        {
            aspect.Clear();

            foreach (var neighbor in otherBoids)
            {
                if (entity == neighbor) continue;
                var transform = state.EntityManager.GetComponentData<LocalTransform>(neighbor);
                if (math.distancesq(aspect.GetPosition(), transform.Position) < math.pow(aspect.m_boid.ValueRO.m_detectionDistance,2))
                {
                    aspect.AddToNeighborList(transform.Position, transform.Forward());
                }
            }
        }
       

        
    }
}


