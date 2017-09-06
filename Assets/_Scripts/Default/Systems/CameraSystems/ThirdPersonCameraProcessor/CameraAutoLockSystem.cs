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
                if (camera != null && camera.hasTransform && camera.hasPivot && camera.hasDirection)
                {
                    var toCamera = camera.direction.value;
                    var autoAngle = GameConfig.instance.cameraAutoDegree;
                    if (camera.hasAngleOffset)
                    {
                        autoAngle -= camera.angleOffset.value;
                    }

                    var pivot = camera.pivot.value;
                    var camTransform = camera.transform.value;
                    if (pivot.hasTransform)
                    {
                        var pivotTransform = pivot.transform.value;
                        var angle = toCamera.AngleFromXZ();
                        var diff = angle - autoAngle;
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