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
    public class MainCameraWalkSystem : ReactiveSystem<InputEntity>
    {
        Contexts _context;

        public MainCameraWalkSystem(Contexts context) : base(context.input)
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
                var angles = camera.transform.value.eulerAngles;
                var rotation = Quaternion.Euler(0f, angles.y, 0f);
                camera.transform.value.position += (rotation * dir * GameConfig.instance.cameraWalk * Time.deltaTime);
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