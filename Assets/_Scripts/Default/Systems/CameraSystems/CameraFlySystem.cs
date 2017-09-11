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
    public class CameraFlySystem : ReactiveSystem<InputEntity>
    {
        GameContext _game;

        public CameraFlySystem(Contexts context) : base(context.input)
        {
            _game = context.game;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var joystick = entities[0];
            var player = _game.playerEntity;
            if (player != null && player.hasCameraTransform)
            {
                var dir = joystick.direction.value;
                dir.z = dir.y;
                dir.y = 0f;
                player.cameraTransform.value.Translate(dir * GameConfig.instance.cameraFlySpeed * Time.deltaTime);
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasDirection && entity.hasName && entity.name.value == "LeftJoystick";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Direction);
        }
    }
}