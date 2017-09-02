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
    public class CameraAutoLockSystem : IExecuteSystem
    {
        GameContext _game;
        float _yVel;

        public CameraAutoLockSystem(Contexts contexts)
        {
            _game = contexts.game;
        }


        public void Execute()
        {
            if (CameraAutoCheckSystem.autoLock)
            {
                var camera = _game.cameraEntity;
                if (camera != null && camera.hasTransform && camera.hasPivot)
                {
                    var pivot = camera.pivot.value;
                    if (pivot.hasTransform)
                    {
                        var camTransform = camera.transform.value;
                        var pivotTransform = pivot.transform.value;
                        var toCamera = camTransform.position - pivotTransform.position;
                        var floor = new Vector3(toCamera.x, 0f, toCamera.z);
                        var angleFromFloor = Vector3.Angle(toCamera, floor);
                        angleFromFloor *= Mathf.Sign(toCamera.y);
                        var diff = angleFromFloor - GameConfig.instance.cameraAutoDegree;
                        var y = Mathf.SmoothDamp(0f, diff * GameConfig.instance.cameraAutoSpeed, ref _yVel, 0.2f);
                        y = Mathf.Abs(diff) > Mathf.Abs(y) ? y : diff;
                        camTransform.RotateAround(pivotTransform.position, Vector3.Cross(Vector3.up, toCamera), y);
                        
                        camera.ReplaceDirection((camTransform.position - pivotTransform.position).normalized);
                    }
                }
            }
        }
    }
}