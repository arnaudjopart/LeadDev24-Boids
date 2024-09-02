

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
        var transforms = m_query.ToComponentDataArray<LocalTransform>(Allocator.Temp);
        var entities = m_query.ToEntityArray(Allocator.Temp);
        


        foreach (var (aspect, boid) in SystemAPI.Query<BoidAspect>().WithEntityAccess())
        {
            aspect.Clear();
            foreach (var entity in entities)
            {
                if (entity == boid) continue;
                var transform = EntityManager.GetComponentData<LocalTransform>(entity);
                
                if (math.distancesq(aspect.GetPosition(), transform.Position) < aspect.m_boid.ValueRO.m_detectionDistance * aspect.m_boid.ValueRO.m_detectionDistance)
                {
                    aspect.AddToNeighborList(transform.Position);
                }
            }
        }
       

        
    }

   protected override void OnDestroy() { }
}


