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
    public class CameraFreeRotationSystem : ReactiveSystem<InputEntity>
    {
        GameContext _game;
        Vector3 _vel;

        public CameraFreeRotationSystem(Contexts context) : base(context.input)
        {
            _game = context.game;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var pad = entities[0];
            var player = _game.playerEntity;
            if (player != null && player.hasCameraTransform)
            {
                var camTransform = player.cameraTransform.value;
                var previous = camTransform.eulerAngles;
                var dir = pad.direction.value;
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    dir.y = 0f;
                }
                var rotation = previous + new Vector3(-dir.y * GameConfig.instance.rightPadY, dir.x * GameConfig.instance.rightPadX, 0f);
                camTransform.eulerAngles = Vector3.SmoothDamp(previous, rotation, ref _vel, 0.2f);
                var angles = camTransform.eulerAngles;
                var angleX = angles.x > 90 ? angles.x - 360 : angles.x;
                var min = -GameConfig.instance.cameraUpDegree;
                var max = -GameConfig.instance.cameraDownDegree;
                angleX = Mathf.Clamp(angleX, min, max);
                angles.x = angleX;
                camTransform.eulerAngles = angles;
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