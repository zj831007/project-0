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
    //after MainCameraFollowMainFigherSystem
    public class MainCameraFreeRotationSystem : ReactiveSystem<InputEntity>
    {
        GameContext _game;
        Vector3 _vel;

        public MainCameraFreeRotationSystem(Contexts context) : base(context.input)
        {
            _game = context.game;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var pad = entities[0];
            var camera = _game.mainCameraEntity;
            if (camera != null && camera.hasTransform)
            {
                var previous = camera.transform.value.eulerAngles;
                var dir = pad.padDirection.value;
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    dir.y = 0f;
                }
                var rotation = previous + new Vector3(-dir.y * GameConfig.instance.rightPadSensitivityX, dir.x * GameConfig.instance.rightPadSensitivityY, 0f);
                camera.transform.value.eulerAngles = Vector3.SmoothDamp(previous, rotation, ref _vel, 0.2f);
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