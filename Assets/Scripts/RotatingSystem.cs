using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct RotatingSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {

    }
    public void OnUpdate(ref SystemState state) 
    { 
        foreach(var (localTransform, rotatingSpeed) in 
            SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotatingSpeedComponentData>>())
        {
            localTransform.ValueRW = localTransform.ValueRW.RotateY(rotatingSpeed.ValueRO.Value*SystemAPI.Time.DeltaTime);
        }
        
    
    }
}


