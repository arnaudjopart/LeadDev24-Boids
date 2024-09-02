using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DemoTagAuthoring : MonoBehaviour
{
    private class Baker : Baker<DemoTagAuthoring>
    {
        public override void Bake(DemoTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new DemoTag());
        }
    }
}
