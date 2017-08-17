using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Project0
{
    [Input]
    public class JoystickDirectionComponent : IComponent
    {
        public Vector3 value;
    }
}