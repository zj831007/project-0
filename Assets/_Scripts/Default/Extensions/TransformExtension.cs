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

namespace Project0
{
    public static class TransformExtension 
    {
        public static GameEntity GetEntity(this Transform transform)
        {
            return transform.GetComponent<EntityCreator>()?.entity;
        }
    }
}