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
        //public Transform fighterCreator;
        public bool isMain;
        public bool hasPivot;

        private void Start()
        {
            var cam = Contexts.sharedInstance.game.CreateEntity();
            cam.isCamera = true;
            cam.isMainCamera = isMain;
            //var dir = transform.position - Vector3.zero;
            //if (fighterCreator)
            {
                //dir = transform.position - fighterCreator.position;
            }
            //cam.AddFighterToCamera(dir);
            cam.AddTransform(transform);
            if (hasPivot)
            {
                var pivot = Contexts.sharedInstance.game.CreateEntity();
                pivot.AddTransform(new GameObject("CameraPivot").transform);
                cam.AddPivot(pivot);
            }
        }
    }
}