using Unity.Entities;
using UnityEngine;

public class RotatingCubeAuthoring : MonoBehaviour
{
    [SerializeField] private float m_rotatingSpeed;
    // Start is called before the first frame update
    private class Baker : Baker<RotatingCubeAuthoring>
    {
        public override void Bake(RotatingCubeAuthoring authoring)
        {
            var currentEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(currentEntity, new RotatingSpeedComponentData
            {
                Value = authoring.m_rotatingSpeed
            });

            AddComponent(currentEntity, new SpeedComponentData
            {
                Value = 5
            }) ;
        }
    }
}


public struct RotatingSpeedComponentData : IComponentData
{
    public float Value;
}
