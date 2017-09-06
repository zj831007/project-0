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
            if (camera != null && camera.hasTransform && camera.hasPivot && camera.hasDistance && camera.hasDirection)
            {
                var pivot = camera.pivot.value;
                if (pivot.hasTransform)
                {
                    var camTransform = camera.transform.value;
                    var pivotTransform = pivot.transform.value;
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
    }
}