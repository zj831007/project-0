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
        float _yVel;//pass

        public CameraAutoLockSystem(Contexts contexts)
        {
        }


        public void Execute()
        {
            if (CameraAutoCheckSystem.autoLock)
            {
                foreach (var camera in ThirdPersonCameraProcessor.cameras.GetEntities())
                {
                    var pivot = camera.GetGamePivotEntity();
                    if (pivot != null && pivot.hasPosition)
                    {
                        var toPivot = camera.direction.value;
                        var autoAngle = GameConfig.instance.cameraAutoDegree;
                        if (camera.hasAngleOffset)
                        {
                            autoAngle += camera.angleOffset.value;
                        }
                        var angle = -toPivot.AngleFromXZ();
                        var diff = angle - autoAngle;
                        var y = Mathf.SmoothDamp(0f, diff * GameConfig.instance.cameraAutoSpeed, ref _yVel, 0.2f);
                        y = Mathf.Abs(diff) > Mathf.Abs(y) ? y : diff;
                        toPivot = Quaternion.AngleAxis(y, Vector3.Cross(toPivot, Vector3.up)) * toPivot;
                        var position = pivot.position.value - toPivot * camera.distance.value;
                        camera.ReplacePosition(position);
                        camera.ReplaceDirection(toPivot);
                    }
                }
            }
        }
    }
}