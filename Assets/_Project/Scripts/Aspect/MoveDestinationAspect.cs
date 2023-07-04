using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Component;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MoveDestinationAspect : IAspect
{
    private readonly Entity _entity;

    private readonly RefRW<LocalTransform> _transform;
    public readonly RefRO<MoveSpeedComponent> speedComponent;
    public readonly RefRW<DestinationComponent> destinationComponent;

    public bool HasReachedDestination()
    {
        return math.distancesq(_transform.ValueRO.Position, destinationComponent.ValueRO.destination) <= 0.1f * 0.1f;
    }

    public void Update(float dt)
    {
        float3 dir = math.normalize(destinationComponent.ValueRO.destination - _transform.ValueRO.Position);
        _transform.ValueRW.Position += dir * speedComponent.ValueRO.speed * dt;
    }
}
