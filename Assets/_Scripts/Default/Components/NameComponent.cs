using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Project0
{
    [Game]
    public class NameComponent : SerializableComponent, IComponent
    {
        public string value;
        public override string ToString()
        {
            return value;
        }
    }
}