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
    public class MainFighterWalkSystem : ReactiveSystem<InputEntity>
    {
        Contexts _context;

        public MainFighterWalkSystem(Contexts context) : base(context.input)
        {
            _context = context;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var joystick = entities[0];
            var camera = _context.game.cameraEntity;
            var fighter = _context.game.mainFighterEntity;
            if (fighter != null && fighter.hasTransform && camera != null && camera.hasTransform)
            {
                var fighterTransform = fighter.transform.value;
                var dir = joystick.direction.value;
                dir.z = dir.y;
                dir.y = 0f;
                var angles = camera.transform.value.eulerAngles;
                var rotation = Quaternion.Euler(0f, angles.y, 0f);
                var dest = fighterTransform.position + rotation * dir * GameConfig.instance.fighterWalkSpeed * Time.deltaTime;
                fighterTransform.LookAt(dest);
                fighterTransform.position = dest;
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