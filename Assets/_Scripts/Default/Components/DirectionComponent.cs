using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Project0
{
    [Game]
    public class DirectionComponent : SerializableComponent, IComponent
    {
        public Vector3 value;
        public override string ToString()
        {
            return "";
        }
    }
}