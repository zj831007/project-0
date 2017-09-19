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
        public CameraTerrainSystem(Contexts contexts)
        {
        }

        public void Execute()
        {
            foreach (var camera in ThirdPersonCameraProcessor.cameras.GetEntities())
            {
                var toCamera = camera.direction.value;
                var autoAngle = GameConfig.instance.cameraAutoDegree;
                var pivot = camera.GetPivotEntity();
                if (pivot != null && pivot.hasPosition)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(pivot.position.value, Vector3.down, out hit, 5f, GameConfig.instance.pawnTerrainMask & ~GameConfig.instance.pawnMask))
                    {
                        var terrain = hit.transform.GetEntity();
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
                }
                if (camera.hasAngleOffset)
                {
                    camera.RemoveAngleOffset();
                }
            }
        }
    }
}