﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Object = UnityEngine.Object;

namespace Project0
{
    [Game]
    public class RotationComponent : SerializableComponent, IComponent
    {
        [DefaultValuePath("rotation")]
        public Quaternion value;
        public override string ToString()
        {
            return "";
        }
    }
}