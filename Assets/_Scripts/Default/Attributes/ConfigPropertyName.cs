using System;
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
using System.Reflection;

namespace Project0
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ConfigPropertyNameAttribute : Attribute
    {
        public string name;

        public ConfigPropertyNameAttribute(string name)
        {
            this.name = name;
        }
    }
}