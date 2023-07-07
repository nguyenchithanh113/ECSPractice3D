using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public partial struct MovingSystem : ISystem
{
    private Random _random;
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _random = new Random(1);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        int moveAspectCount = 0;

        foreach (var VARIABLE in SystemAPI.Query<MoveDestinationAspect>())
        {
            moveAspectCount++;
        }
        
        
        NativeArray<float3> rands = new NativeArray<float3>(moveAspectCount, Allocator.TempJob);

        for (int i = 0; i < moveAspectCount; i++)
        {
            rands[i] = GetRandomPosition();
        }

        float dt = SystemAPI.Time.DeltaTime;

        JobHandle moveJobHandle = new MoveJob()
        {
            dt = dt,
        }.ScheduleParallel(state.Dependency);

        JobHandle checkJobHandle =  new CheckReachDestiantionJob()
        {
            randPosition = rands,
        }.ScheduleParallel(moveJobHandle);
        
        checkJobHandle.Complete();

        rands.Dispose();

    }
    
    [BurstCompile]
    public float3 GetRandomPosition()
    {
        return new float3(
            _random.NextFloat(-15f, 15f),
            0,
            _random.NextFloat(-15f, 15f)
        );
    }

}

[BurstCompile]
public partial struct MoveJob : IJobEntity
{
    public float dt;
    
    [BurstCompile]
    public void Execute(MoveDestinationAspect moveAspect)
    {
        moveAspect.Update(dt);
    }
}
[BurstCompile]
public partial struct  CheckReachDestiantionJob : IJobEntity
{
    public NativeArray<float3> randPosition;
    
    [BurstCompile]
    public void Execute([EntityIndexInQuery] int entityIndexInQuery, MoveDestinationAspect moveAspect)
    {
        if (moveAspect.HasReachedDestination())
        {
            moveAspect.destinationComponent.ValueRW.destination = randPosition[entityIndexInQuery];
        }
    }
}

