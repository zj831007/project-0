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
    public class CameraLiftSystem : ReactiveSystem<GameEntity>
    {
        public CameraLiftSystem(Contexts context) : base(context.game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var camera = e.GetCameraEntity();
                if (camera != null && camera.hasPosition)
                {
                    camera.ReplacePosition(camera.position.value + e.direction.value * GameConfig.instance.cameraLiftSpeed);
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasInputType && entity.inputType.value == InputType.VerticalMovement && entity.hasDirection;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Direction);
        }
    }
}