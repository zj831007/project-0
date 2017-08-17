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
    public class MainFighterFlySystem : ReactiveSystem<InputEntity>
    {
        Contexts _context;

        public MainFighterFlySystem(Contexts context) : base(context.input)
        {
            _context = context;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var joystick = entities[0];
            var fighter = _context.game.mainFighterEntity;
            var camera = _context.game.mainCameraEntity;
            if (fighter != null && fighter.hasTransform && camera != null && camera.hasTransform)
            {
                var dir = joystick.joystickDirection.value;
                dir.z = dir.y;
                dir.y = 0f;
                var angles = camera.transform.value.eulerAngles;
                var rotation = Quaternion.Euler(angles.x, angles.y, 0f);
                fighter.transform.value.Translate(rotation * dir * GameConfig.instance.fighterFlySpeed * Time.deltaTime);
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasJoystickDirection && entity.hasName && entity.name.value == "Left";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.JoystickDirection);
        }
    }
}