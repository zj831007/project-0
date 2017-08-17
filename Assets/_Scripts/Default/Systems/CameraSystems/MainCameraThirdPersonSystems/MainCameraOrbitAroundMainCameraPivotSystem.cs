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
    public class MainCameraOrbitAroundMainCameraPivotSystem : ReactiveSystem<InputEntity>
    {
        GameContext _game;
        float _xVel;
        float _yVel;

        public MainCameraOrbitAroundMainCameraPivotSystem(Contexts context) : base(context.input)
        {
            _game = context.game;
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
            var camera = _game.mainCameraEntity;
            var fighter = _game.mainFighterEntity;
            if (camera != null && camera.hasTransform && fighter != null && fighter.hasTransform)
            {
                var previous = camera.transform.value.eulerAngles;
                var dir = pad.padDirection.value;
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    dir.y = 0f;
                }
                var camTransform = camera.transform.value;
                var fighterTransform = fighter.transform.value;
                var x = Mathf.SmoothDamp(0f, dir.x, ref _xVel, 0.2f) * GameConfig.instance.rightPadSensitivityX;
                var y = Mathf.SmoothDamp(0f, dir.y, ref _yVel, 0.2f) * GameConfig.instance.rightPadSensitivityY;
                var toCamera = camTransform.position - fighterTransform.position;
                var floor = new Vector3(toCamera.x, 0f, toCamera.z);
                var angleFromFloor = Vector3.Angle(toCamera, floor);
                angleFromFloor *= Mathf.Sign(toCamera.y);
                var min = GameConfig.instance.mainCameraDownThresold;
                var max = GameConfig.instance.mainCameraUpThresold;
                y = Math.Min(angleFromFloor-min, Mathf.Max(angleFromFloor - max, y));
                camTransform.RotateAround(fighterTransform.position, Vector3.up, x);
                camTransform.RotateAround(fighterTransform.position, Vector3.Cross(Vector3.up, toCamera), y);
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasPadDirection && entity.hasName && entity.name.value == "Right";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.PadDirection);
        }
    }
}