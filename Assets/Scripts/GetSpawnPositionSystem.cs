
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


public partial struct GetSpawnPositionSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnerComponentData>();
    }




    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerComponentData>();
        var bufferOfSpawnPosition = state.EntityManager.GetBuffer<SpawnPositionElement>(spawnerEntity);
        
        foreach (var item in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<SpawnPositionTag>())
        {
            bufferOfSpawnPosition.Add(new SpawnPositionElement
            {
                Value = item.ValueRO.Position
            });
        }
       
    }
    /*
    protected override void OnUpdate()
    {
        Enabled = false;
        var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerComponentData>();
        var bufferOfSpawnPosition = SystemAPI.GetBuffer<SpawnPositionElement>(spawnerEntity);

        foreach (var item in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<SpawnPositionTag>())
        {
            bufferOfSpawnPosition.Add(new SpawnPositionElement
            {
                Value = item.ValueRO.Position
            });
        }
    }*/
}

public partial class RandomSpawnSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireForUpdate<SpawnerComponentData>();

    }
    protected override void OnUpdate()
    {

        
        var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerComponentData>();
        var aspect = SystemAPI.GetAspect<SpawnerAspect>(spawnerEntity);
        aspect.UpdateTimer(SystemAPI.Time.DeltaTime);
        if(aspect.IsReady)
        {
            var value = aspect.GetRandomPosition();
            Debug.Log(value);

        }
    }
}