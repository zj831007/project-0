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
    public class MainCameraCenterOnMainFighterSystem : ReactiveSystem<InputEntity>
    {
        Contexts _context;
        Vector3 _dragVel;
        Vector3 _moveVel;
        Vector3 _drag;

        public MainCameraCenterOnMainFighterSystem(Contexts context) : base(context.input)
        {
            _context = context;
        }

        Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num = 2f / smoothTime;
            float num2 = num * deltaTime;
            float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            Vector3 vector = current - target;
            Vector3 vector2 = target;
            float maxLength = maxSpeed * smoothTime;
            vector = Vector3.ClampMagnitude(vector, maxLength);
            target = current - vector;
            Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            Vector3 vector4 = target + (vector + vector3) * d;
            if (Vector3.Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }
            return vector4;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var pad = entities[0];
            var camera = _context.game.mainCameraEntity;
            if (camera.hasParent)
            {
                var cameraRig = camera.parent.value;
                var fighter = _context.game.mainFighterEntity;
                if (camera != null && camera.hasTransform && fighter != null && fighter.hasTransform)
                {
                    var previous = camera.transform.value.eulerAngles;
                    var dir = pad.padDirection.value;
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        dir.y = 0f;
                    }
                    _drag += new Vector3(dir.y * GameConfig.instance.rightPadSensitivityX, dir.x * GameConfig.instance.rightPadSensitivityY, 0f);
                    _drag.x = Mathf.Max(-60, Mathf.Min(60, _drag.x));
                    var camTransform = camera.transform.value;
                    var fighterTransform = fighter.transform.value;
                    var focus = fighterTransform.position + GameConfig.instance.mainCameraHeightVector;
                    var toCamera = camTransform.position - focus;
                    //cameraRig.position = fighterTransform.position + toCamera;
                    var dest = focus + Quaternion.Euler(_drag) * GameConfig.instance.mainCameraDistanceVector;

                    cameraRig.position = dest;
                    //dest = Vector3.SmoothDamp(camTransform.position, dest, ref _dragVel, 0.2f);
                    //camTransform.position = dest;
                    //camera.ReplaceFighterToCamera(dest - fighterTransform.position);
                    //camTransform.position = Vector3.Slerp(camTransform.position, dest, 5f * Time.deltaTime);
                    //camTransform.LookAt(focus);
                    //dest = Vector3.SmoothDamp(dest,, ref _moveVel, 0.2f);
                    //camTransform.position=Vector3.SmoothDamp()
                    //cameraRig.position = Vector3.SmoothDamp(cameraRig.position, dest, ref _vel, 0.2f);
                    //camTransform.position = Vector3.one;

                    //Debug.Log(camTransform.position);
                    //camTransform.position = Vector3.one * 2;

                    //Debug.Log(camTransform.position);
                }
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