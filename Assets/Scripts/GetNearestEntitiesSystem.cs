

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

        foreach (var aspect in SystemAPI.Query<BoidAspect>())
        {
            aspect.Clear();
            Debug.Log(aspect.m_boid.ValueRO.m_detectionDistance);
            foreach (var transform in transforms)
            {
                if (math.distancesq(aspect.GetPosition(), transform.Position) < aspect.m_boid.ValueRO.m_detectionDistance * aspect.m_boid.ValueRO.m_detectionDistance)
                {
                    aspect.AddToNeighborList(transform.Position);
                }
            }
        }
       

        
    }

   protected override void OnDestroy() { }
}


