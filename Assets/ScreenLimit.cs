using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ScreenLimit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var bottomLeftPoint = Camera.main.ViewportToWorldPoint(Vector2.zero);
        var topRightPoint = Camera.main.ViewportToWorldPoint(Vector2.one);

        var screenWidth = Mathf.Abs(bottomLeftPoint.x - topRightPoint.x);
        var screenHeight = Mathf.Abs(bottomLeftPoint.z - topRightPoint.z);

        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var screenLimitEntity = entityManager.CreateSingleton<ScreenLimitsComponentData>(new ScreenLimitsComponentData()
        {
            bottomLeftLimit = Camera.main.ViewportToWorldPoint(Vector2.zero),
            topRightLimit = Camera.main.ViewportToWorldPoint(Vector2.one),
            screenWidth = screenWidth,
            screenHeight = screenHeight

        }, "ScreenLimits"); 
        
        
    }
}

struct ScreenLimitsComponentData : IComponentData
{
    public float3 bottomLeftLimit; 
    public float3 topRightLimit;
    public float screenWidth;
    public float screenHeight;
}