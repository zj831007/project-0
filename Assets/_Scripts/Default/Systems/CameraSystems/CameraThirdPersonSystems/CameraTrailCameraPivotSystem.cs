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
    public class CameraTrailCameraPivotSystem : IExecuteSystem
    {
        GameContext _game;

        public CameraTrailCameraPivotSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var camera = _game.cameraEntity;
            var fighter = _game.mainFighterEntity;
            if (camera != null && camera.hasTransform && camera.hasPivot)
            {
                var pivot = camera.pivot.value;
                if (pivot.hasTransform)
                {
                    var camTransform = camera.transform.value;
                    var pivotTransform = pivot.transform.value;
                    Vector3 dir;
                    if (camera.hasDirection)
                    {
                        dir = camera.direction.value;
                    }
                    else
                    {
                        dir = (camTransform.position - pivotTransform.position).normalized;
                        camera.AddDirection(dir);
                    }
                    camTransform.position = pivotTransform.position + dir * GameConfig.instance.cameraDistance;
                }
            }
        }
    }
}