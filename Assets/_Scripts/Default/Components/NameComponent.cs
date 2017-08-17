using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Project0
{
    [Game, Input]
    public class NameComponent : IComponent
    {
        public string value;
    }
}