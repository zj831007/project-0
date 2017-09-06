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
    public class CameraTerrainSystem : IExecuteSystem
    {
        GameContext _game;
        float _yVel;

        public CameraTerrainSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var camera = _game.cameraEntity;
            if (camera != null && camera.hasDirection && camera.hasPivot)
            {
                var toCamera = camera.direction.value;
                var pivot = camera.pivot.value;
                var autoAngle = GameConfig.instance.cameraAutoDegree;
                if (pivot.hasTransform)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(pivot.transform.value.position, Vector3.down, out _hit, 5f, GameConfig.instance.fighterTerrainMask))
                    {
                        var terrain = _hit.transform.getGameEntity();
                        if (terrain != null && terrain.hasNormal)
                        {
                            var terrainNormal = terrain.normal.value;
                            var planeNormal = Vector3.Cross(Vector3.up, toCamera);
                            var planeAngle = Vector3.Angle(Vector3.up, Vector3.ProjectOnPlane(terrainNormal, planeNormal));
                            var angleOffset = planeAngle * Mathf.Sign(Vector3.Dot(terrainNormal.ToXZ(), toCamera.ToXZ()));
                            autoAngle -= angleOffset;
                            camera.ReplaceAngleOffset(angleOffset);
                            return;
                        }
                    }
                    if (camera.hasAngleOffset)
                    {
                        camera.RemoveAngleOffset();
                    }
                }
            }
        }
    }
}