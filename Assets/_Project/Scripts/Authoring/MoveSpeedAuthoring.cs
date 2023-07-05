using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Component;
using Unity.Entities;
using UnityEngine;

public class MoveSpeedAuthoring : MonoBehaviour
{
    public float speed;
}

public class MoveSpeedAuthoringBaker : Baker<MoveSpeedAuthoring>
{
    public override void Bake(MoveSpeedAuthoring authoring)
    {
        AddComponent(GetEntity(TransformUsageFlags.Dynamic),new MoveSpeedComponent()
        {
            speed = authoring.speed
        });
    }
}
