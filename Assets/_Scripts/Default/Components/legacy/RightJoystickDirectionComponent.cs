using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Project0
{
    [Input, Unique]
    public class RightJoystickDirectionComponent : IComponent
    {
        public Vector3 value;
    }
}