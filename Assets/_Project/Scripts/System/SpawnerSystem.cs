using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public partial struct SpawnerSystem : ISystem, ISystemStartStop
{
    private Random _random;
    private SpawnerData _spawnerData;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnerData>();
        state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        _random = new Random(2);
        
    }
    
    

    public void OnDestroy(ref SystemState state)
    {

    }

    public void OnStartRunning(ref SystemState state)
    {
        _spawnerData = SystemAPI.GetSingleton<SpawnerData>();
        EntityCommandBuffer commandBuffer = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);

        
        for (int i = 0; i < 300; i++)
        {
            Entity entity = commandBuffer.Instantiate(_spawnerData.entityPrefab);
            commandBuffer.SetComponent(entity, new MoveSpeedComponent()
            {
                speed = _random.NextFloat(5,20)
            });
        }
    }

    public void OnStopRunning(ref SystemState state)
    {
        
    }
}
