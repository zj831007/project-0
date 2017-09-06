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
        GameContext _game;
        float _xVel;
        float _yVel;

        public CameraOrbitAroundCameraPivotSystem(Contexts contexts) : base(contexts.input)
        {
            _game = contexts.game;
        }

        //Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        //{
        //    smoothTime = Mathf.Max(0.0001f, smoothTime);
        //    float num = 2f / smoothTime;
        //    float num2 = num * deltaTime;
        //    float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
        //    Vector3 vector = current - target;
        //    Vector3 vector2 = target;
        //    float maxLength = maxSpeed * smoothTime;
        //    vector = Vector3.ClampMagnitude(vector, maxLength);
        //    target = current - vector;
        //    Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
        //    currentVelocity = (currentVelocity - num * vector3) * d;
        //    Vector3 vector4 = target + (vector + vector3) * d;
        //    if (Vector3.Dot(vector2 - current, vector4 - vector2) > 0f)
        //    {
        //        vector4 = vector2;
        //        currentVelocity = (vector4 - vector2) / deltaTime;
        //    }
        //    return vector4;
        //}

        protected override void Execute(List<InputEntity> entities)
        {
            var pad = entities[0];
            var camera = _game.cameraEntity;
            if (camera != null && camera.hasTransform && camera.hasPivot && camera.hasDirection)
            {
                var pivot = camera.pivot.value;
                if (pivot.hasTransform)
                {
                    var dir = pad.direction.value;
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        dir.y = 0f;
                    }
                    var camTransform = camera.transform.value;
                    var pivotTransform = pivot.transform.value;
                    var x = Mathf.SmoothDamp(0f, dir.x, ref _xVel, 0.2f) * GameConfig.instance.rightPadX;
                    var y = Mathf.SmoothDamp(0f, dir.y, ref _yVel, 0.2f) * GameConfig.instance.rightPadY;
                    var toCamera = camera.direction.value;
                    var angle = toCamera.AngleFromXZ() * Mathf.Sign(toCamera.y);
                    var min = GameConfig.instance.cameraDownDegree;
                    var max = GameConfig.instance.cameraUpDegree;
                    y = Math.Min(angle - min, Mathf.Max(angle - max, y));
                    camTransform.RotateAround(pivotTransform.position, Vector3.up, x);
                    camTransform.RotateAround(pivotTransform.position, Vector3.Cross(Vector3.up, toCamera), y);
                    camera.ReplaceDirection((camTransform.position - pivotTransform.position).normalized);
                }
            }
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