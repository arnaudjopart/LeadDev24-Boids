using Unity.Entities;
using Unity.Transforms;

public partial struct MoveForwardSystem : ISystem
{
    void OnUpdate(ref SystemState state)
    {
        foreach(var (localTransform, speed) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<SpeedComponentData>>())
        {
            var forwardDirection = localTransform.ValueRW.Forward();
            localTransform.ValueRW.Position += forwardDirection * SystemAPI.Time.DeltaTime * speed.ValueRO.Value;
        }
    }
}


public struct SpeedComponentData : IComponentData
{
    public float Value;
}