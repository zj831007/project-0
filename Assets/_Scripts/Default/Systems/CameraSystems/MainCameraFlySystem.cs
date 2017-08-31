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
    public class MainCameraFlySystem : ReactiveSystem<InputEntity>
    {
        Contexts _context;

        public MainCameraFlySystem(Contexts context) : base(context.input)
        {
            _context = context;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var joystick = entities[0];
            var camera = _context.game.mainCameraEntity;
            if (camera != null && camera.hasTransform)
            {
                var dir = joystick.direction.value;
                dir.z = dir.y;
                dir.y = 0f;
                camera.transform.value.Translate(dir * GameConfig.instance.cameraFly * Time.deltaTime);
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