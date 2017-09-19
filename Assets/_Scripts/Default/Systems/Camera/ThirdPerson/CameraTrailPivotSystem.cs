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
    public class CameraTrailPivotSystem : IExecuteSystem
    {
        public CameraTrailPivotSystem(Contexts contexts)
        {
        }

        public void Execute()
        {
            foreach (var camera in ThirdPersonCameraProcessor.cameras.GetEntities())
            {
                var pivot = camera.GetPivotEntity();
                if (pivot != null && pivot.hasPosition)
                {
                    var oldDir = camera.direction.value;
                    var newDir = pivot.position.value - camera.position.value;
                    var angle = oldDir.AngleFromXZ();
                    var floor = newDir.ToXZ();
                    newDir = (Quaternion.AngleAxis(angle, Vector3.Cross(floor, Vector3.up)) * floor).normalized;
                    camera.ReplacePosition(pivot.position.value - newDir * camera.distance.value);
                    camera.ReplaceDirection(newDir);
                }
            }
        }
    }
}