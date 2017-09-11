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
            var camera = ThirdPersonCameraProcessor.camera;
            var camTransform = camera.transform.value;
            var pivotTransform = camera.pivotTransform.value;
            var oldDir = camera.direction.value;
            var newDir = camTransform.position - pivotTransform.position;
            var angle = oldDir.AngleFromXZ();
            var floor = newDir.ToXZ();
            newDir = (Quaternion.AngleAxis(angle, Vector3.Cross(floor, Vector3.up)) * floor).normalized;
            camTransform.position = pivotTransform.position + newDir * camera.distance.value;
            camera.ReplaceDirection(newDir);
        }
    }
}