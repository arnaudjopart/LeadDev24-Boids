using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CameraTargetTagAuthoring : MonoBehaviour
{
    private class Baker : Baker<CameraTargetTagAuthoring>
    {
        public override void Bake(CameraTargetTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CameraTargetTagComponentData());
        }
    }

}


struct CameraTargetTagComponentData : IComponentData
{

}

