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
                    var pivot = camera.pivot.value;
                    if (pivot.hasTransform)
                    {
                        var camTransform = camera.transform.value;
                        var pivotTransform = pivot.transform.value;
                        var toCamera = camera.direction.value;
                        var autoAngle = GameConfig.instance.cameraAutoDegree;
                        if (camera.isUseTerrainNormal)
                        {
                            var figher = _game.mainFighterEntity;
                            if (figher != null && figher.hasTransform)
                            {
                                RaycastHit _hit;
                                if (Physics.Raycast(figher.transform.value.position, Vector3.down, out _hit, 5f, GameConfig.instance.fighterTerrainMask))
                                {
                                    var planeNormal = Vector3.Cross(Vector3.up, camera.direction.value);
                                    var planeAngle = Vector3.Angle(Vector3.up, Vector3.ProjectOnPlane(_hit.normal, planeNormal));
                                    autoAngle -= planeAngle;
                                }
                            }
                        }
                        var angle = toCamera.AngleFromXZ() * Mathf.Sign(toCamera.y);
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