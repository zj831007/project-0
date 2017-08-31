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
    public class MainCameraTrailMainCameraPivotSystem : IExecuteSystem
    {
        GameContext _game;

        public MainCameraTrailMainCameraPivotSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var camera = _game.mainCameraEntity;
            var fighter = _game.mainFighterEntity;
            if (camera != null && camera.hasTransform && camera.hasPivot)
            {
                var pivot = camera.pivot.value;
                if (pivot.hasTransform)
                {
                    var camTransform = camera.transform.value;
                    var pivotTransform = pivot.transform.value;
                    var toCamera = camTransform.position - pivotTransform.position;
                    camTransform.position = pivotTransform.position + toCamera.normalized * GameConfig.instance.cameraDistance;
                }
            }
        }
    }
}