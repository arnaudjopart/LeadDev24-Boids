using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class CameraTargetEntityProviderSystem : SystemBase
{

    protected override void OnCreate()
    {
        base.OnCreate();
        
    }
    protected override void OnUpdate()
    {
        Enabled = false;
        var singletonEntity = SystemAPI.GetSingletonEntity<CameraTargetCollectionComponentData>();
        var buffer = SystemAPI.GetBuffer<CameraTargetBufferElement>(singletonEntity);
        
        foreach (var (localTransform,entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<CameraTargetTagComponentData>().WithEntityAccess()){
            buffer.Add(new CameraTargetBufferElement()
            {
                entity = entity
            });
        }
        
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}

public partial class CameraSwitchSystem : SystemBase
{
    public event Action<Entity> OnEntitySelection;
    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            foreach(var aspect in SystemAPI.Query<CameraSwitchAspect>())
            {
                var entity = aspect.GetNextEntity();

                OnEntitySelection?.Invoke(entity);
            }
        }
    }
}