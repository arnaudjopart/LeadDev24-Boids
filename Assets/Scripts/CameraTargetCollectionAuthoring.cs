using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CameraTargetCollectionAuthoring : MonoBehaviour
{
    private class Baker : Baker<CameraTargetCollectionAuthoring>
    {
        public override void Bake(CameraTargetCollectionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new CameraTargetCollectionComponentData());
            AddBuffer<CameraTargetBufferElement>(entity);

        }
    }
}


public struct CameraTargetCollectionComponentData : IComponentData
{
    public int currentCollectionIndex;
}

public struct CameraTargetBufferElement : IBufferElementData
{
    public Entity entity;
}

public readonly partial struct CameraSwitchAspect : IAspect
{
    public readonly RefRW<CameraTargetCollectionComponentData> m_cameraTargetIndex;
    public readonly DynamicBuffer<CameraTargetBufferElement> m_listOfCameraTargetEntities;

    internal Entity GetNextEntity()
    {
        var nextIndex = m_cameraTargetIndex.ValueRO.currentCollectionIndex+1;
        nextIndex %= m_listOfCameraTargetEntities.Length;
        m_cameraTargetIndex.ValueRW.currentCollectionIndex = nextIndex;
        return m_listOfCameraTargetEntities[nextIndex].entity;
    }
}