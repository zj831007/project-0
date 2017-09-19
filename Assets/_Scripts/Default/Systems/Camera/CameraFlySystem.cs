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
    public class CameraFlySystem : ReactiveSystem<GameEntity>
    {
        public CameraFlySystem(Contexts context) : base(context.game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var camera = e.GetCameraEntity();
                if (camera != null && camera.hasPosition)
                {
                    var dir = e.direction.value;
                    dir.z = dir.y;
                    dir.y = 0f;
                    if (camera.hasRotation)
                    {
                        dir = camera.rotation.value * dir;
                    }
                    camera.ReplacePosition(camera.position.value + dir * GameConfig.instance.cameraFlySpeed);
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasInputType && entity.inputType.value == InputType.AirMovement && entity.hasDirection;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Direction);
        }
    }
}