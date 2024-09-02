using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct  WrapSystem : ISystem
{
    private void Oncreate(ref SystemState state)
    {
        state.RequireForUpdate<ScreenLimitsComponentData>();
    }

    private void OnUpdate(ref SystemState state)
    {
        var screenLimitsSingelton = SystemAPI.GetSingleton<ScreenLimitsComponentData>();
        foreach (var localTransform in SystemAPI.Query<RefRW<LocalTransform>>())
        {
            if(localTransform.ValueRO.Position.x<screenLimitsSingelton.bottomLeftLimit.x 
                && localTransform.ValueRO.Forward().x < 0)
            {
                var wrapPosition = new float3(localTransform.ValueRO.Position.x+screenLimitsSingelton.screenWidth, 0, localTransform.ValueRO.Position.z);
                localTransform.ValueRW.Position = wrapPosition;
            }

            if (localTransform.ValueRO.Position.z < screenLimitsSingelton.bottomLeftLimit.z
                && localTransform.ValueRO.Forward().z < 0)
            {
                var wrapPosition = new float3(localTransform.ValueRO.Position.x, 0, localTransform.ValueRO.Position.z +screenLimitsSingelton.screenHeight);
                localTransform.ValueRW.Position = wrapPosition;
            }

            if (localTransform.ValueRO.Position.z > screenLimitsSingelton.topRightLimit.z
                && localTransform.ValueRO.Forward().z > 0)
            {
                var wrapPosition = new float3(localTransform.ValueRO.Position.x, 0, localTransform.ValueRO.Position.z - screenLimitsSingelton.screenHeight);
                localTransform.ValueRW.Position = wrapPosition;
            }

            if (localTransform.ValueRO.Position.x > screenLimitsSingelton.topRightLimit.x
                && localTransform.ValueRO.Forward().x > 0)
            {
                var wrapPosition = new float3(localTransform.ValueRO.Position.x-screenLimitsSingelton.screenWidth, 0, localTransform.ValueRO.Position.z);
                localTransform.ValueRW.Position = wrapPosition;
            }
        }
    }

    private void OnDestroy(ref SystemState state)
    {

    }
}


