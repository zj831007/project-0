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
    public class CameraLiftSystem : ReactiveSystem<InputEntity>
    {
        Contexts _context;

        public CameraLiftSystem(Contexts context) : base(context.input)
        {
            _context = context;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var tuple = entities[0];
            var camera = _context.game.cameraEntity;
            if (camera != null && camera.hasTransform)
            {
                camera.transform.value.Translate(tuple.direction.value * GameConfig.instance.cameraLiftSpeed * Time.deltaTime);
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasDirection && entity.hasName && entity.name.value == "LeftTupleButton";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Direction);
        }
    }
}