using Unity.Entities;

internal readonly partial struct SpawnerAspect : IAspect
{
    public readonly RefRW<SpawnerComponentData> m_spawner;
    public readonly DynamicBuffer<SpawnPositionElement> m_spawnPositions;
    public readonly RefRW<TimerComponentData> m_timerComponent;

    public void UpdateTimer(float _deltatime)
    {
        m_timerComponent.ValueRW.m_time+= _deltatime;
    }

    public bool IsReady => m_timerComponent.ValueRW.m_time > 5;
    
    public int GetRandomPosition()
    {
        var test = m_spawnPositions.Length;
        var value = m_spawner.ValueRW.m_random.NextInt(0,test);
        return value;
    }
}

public struct TimerComponentData : IComponentData
{
    public float m_time;
}