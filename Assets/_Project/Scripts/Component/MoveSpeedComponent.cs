using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Component
{
    public struct MoveSpeedComponent : IComponentData
    {
        public float speed;
    }
}

