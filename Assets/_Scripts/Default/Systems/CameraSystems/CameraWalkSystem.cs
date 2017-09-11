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
    public class CameraWalkSystem : ReactiveSystem<InputEntity>
    {
        GameContext _game;

        public CameraWalkSystem(Contexts context) : base(context.input)
        {
            _game = context.game;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var joystick = entities[0];
            var player = _game.playerEntity;
            if (player != null && player.hasCameraTransform)
            {
                var camTransform = player.cameraTransform.value;
                var dir = joystick.direction.value;
                dir.z = dir.y;
                dir.y = 0f;
                var angles = camTransform.eulerAngles;
                var rotation = Quaternion.Euler(0f, angles.y, 0f);
                camTransform.position += (rotation * dir * GameConfig.instance.cameraWalkSpeed * Time.deltaTime);
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasDirection && entity.hasName && entity.name.value == "RightJoystick";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Direction);
        }
    }
}