﻿using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Project0
{
    [Input, Unique]
    public class RightJoystickTouchingComponent : IComponent
    {
        public bool value;
    }
}