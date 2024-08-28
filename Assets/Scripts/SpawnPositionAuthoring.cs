using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnPositionAuthoring : MonoBehaviour
{

    private class Baker : Baker<SpawnPositionAuthoring>
    {
        public override void Bake(SpawnPositionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpawnPositionTag { });
        }
    }
}


public struct SpawnPositionTag : IComponentData
{

}
