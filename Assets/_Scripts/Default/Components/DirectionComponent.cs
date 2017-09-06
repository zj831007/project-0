using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Project0
{
    [Game, Input]
    public class DirectionComponent : IComponent
    {
        public Vector3 value;
        public override string ToString()
        {
            return "";
        }
    }
}