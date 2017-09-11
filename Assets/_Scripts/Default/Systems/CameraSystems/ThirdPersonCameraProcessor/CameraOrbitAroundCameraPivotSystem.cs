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
    public class CameraOrbitAroundCameraPivotSystem : ReactiveSystem<InputEntity>
    {
        float _xVel;
        float _yVel;

        public CameraOrbitAroundCameraPivotSystem(Contexts contexts) : base(contexts.input)
        {
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var pad = entities[0];
            var camera = ThirdPersonCameraProcessor.camera;
            var dir = pad.direction.value;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir.y = 0f;
            }
            var camTransform = camera.transform.value;
            var pivotTransform = camera.pivotTransform.value;
            var x = Mathf.SmoothDamp(0f, dir.x, ref _xVel, 0.2f) * GameConfig.instance.rightPadX;
            var y = Mathf.SmoothDamp(0f, dir.y, ref _yVel, 0.2f) * GameConfig.instance.rightPadY;
            var toCamera = camera.direction.value;
            var angle = toCamera.AngleFromXZ();
            var min = GameConfig.instance.cameraDownDegree;
            var max = GameConfig.instance.cameraUpDegree;
            if (camera.hasAngleOffset)
            {
                min = Mathf.Max(-89f, min - camera.angleOffset.value);
                //max = Mathf.Min(89f, max - camera.angleOffset.value);
            }
            y = Math.Min(angle - min, Mathf.Max(angle - max, y));
            camTransform.RotateAround(pivotTransform.position, Vector3.up, x);
            camTransform.RotateAround(pivotTransform.position, Vector3.Cross(Vector3.up, toCamera), y);
            camera.ReplaceDirection((camTransform.position - pivotTransform.position).normalized);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasDirection && entity.hasName && entity.name.value == "RightPad";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Direction);
        }
    }
}