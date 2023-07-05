using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject entityPrefab;
}

public struct SpawnerData : IComponentData
{
    public Entity entityPrefab;
}

public class SpawnerBaker : Baker<SpawnerAuthoring>
{
    public override void Bake(SpawnerAuthoring authoring)
    {
        Entity prefab = GetEntity(authoring.entityPrefab,TransformUsageFlags.Dynamic);
        AddComponent(GetEntity(TransformUsageFlags.Dynamic), new SpawnerData()
        {
            entityPrefab = prefab
        });
    }
}
