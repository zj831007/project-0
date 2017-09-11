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

namespace Project0.EntityCreators
{
    public class CameraCreator : GameEntityCreator
    {
        public Transform pivot;

        private void Awake()
        {
            entity = Contexts.sharedInstance.game.CreateEntity();
            entity.AddTransform(transform);
            if (pivot)
            {
                entity.AddPivotTransform(pivot);
            }
        }
    }
}