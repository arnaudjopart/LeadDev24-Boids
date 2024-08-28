using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class CameratFollowEntity : MonoBehaviour
{
    private Entity m_currentTarget;
    private EntityManager m_entityManager;
    [SerializeField] private Transform m_pointer;

    // Start is called before the first frame update
    void Start()
    {
        m_entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        CameraSwitchSystem cameraSwitchSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<CameraSwitchSystem>();
        if (cameraSwitchSystem == null)
        {
            Debug.LogError("mising CameraSwitchSystem");
            return;
        }
        cameraSwitchSystem.OnEntitySelection += OnEntitySelection;
    }

    private void OnEntitySelection(Entity entity)
    {
        m_currentTarget = entity;
        Debug.Log(World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(m_currentTarget).Position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_entityManager.Exists(m_currentTarget)) return;
        m_pointer.position = m_entityManager.GetComponentData<LocalTransform>(m_currentTarget).Position;

    }
}
