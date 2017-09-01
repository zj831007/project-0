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
        private void Awake()
        {
            var cam = Contexts.sharedInstance.game.CreateEntity();
            cam.isCamera = true;
            cam.AddTransform(transform);
        }
    }
}