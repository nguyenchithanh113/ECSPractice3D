using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Component;
using Unity.Entities;
using UnityEngine;

public class DestinationAuthoring : MonoBehaviour
{
    
}

public class DestinationAuthoringBaker : Baker<DestinationAuthoring>
{
    public override void Bake(DestinationAuthoring authoring)
    {
        AddComponent(new DestinationComponent());
    }
}
