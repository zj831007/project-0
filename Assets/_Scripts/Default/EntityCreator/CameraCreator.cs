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
    public class CameraCreator : MonoBehaviour
    {
        public bool isMain;

        private void Start()
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();
            entity.isCamera = true;
            entity.isMainCamera = isMain;
            if (transform.parent != null)
            {
                entity.AddParent(transform.parent);
            }
            entity.AddTransform(transform);
        }
    }
}