

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class GetNearestEntitiesSystem : SystemBase
{
    private EntityQuery m_query;

    protected override void OnCreate()
    {
        m_query = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<LocalTransform>()
            .WithAll<Boid>()
            .Build(this);
    }
    protected override void OnUpdate()
    {
        var otherBoids = m_query.ToEntityArray(Allocator.Temp);

        foreach (var (aspect,entity) in SystemAPI.Query<BoidAspect>().WithEntityAccess())
        {
            aspect.Clear();

            foreach (var neighbor in otherBoids)
            {
                if (entity == neighbor) continue;
                var transform = EntityManager.GetComponentData<LocalTransform>(neighbor);
                if (math.distancesq(aspect.GetPosition(), transform.Position) < aspect.m_boid.ValueRO.m_detectionDistance * aspect.m_boid.ValueRO.m_detectionDistance)
                {
                    aspect.AddToNeighborList(transform.Position);
                }
            }
        }
       

        
    }

   protected override void OnDestroy() { }
}


