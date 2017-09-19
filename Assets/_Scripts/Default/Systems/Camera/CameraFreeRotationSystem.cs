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
    public class CameraFreeRotationSystem : ReactiveSystem<GameEntity>
    {
        public CameraFreeRotationSystem(Contexts context) : base(context.game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var pad = entities[0];
            foreach (var e in entities)
            {
                var camera = e.GetCameraEntity();
                if (camera != null && camera.hasRotation)
                {
                    var dir = pad.direction.value;
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        dir.y = 0f;
                    }
                    var angles = camera.rotation.value.eulerAngles + new Vector3(-dir.y * GameConfig.instance.rightPadY, dir.x * GameConfig.instance.rightPadX, 0f);
                    var angleX = angles.x > 90 ? angles.x - 360 : angles.x;
                    var min = -GameConfig.instance.cameraUpDegree;
                    var max = -GameConfig.instance.cameraDownDegree;
                    angles.x = Mathf.Clamp(angleX, min, max);
                    camera.ReplaceRotation(Quaternion.Euler(angles));
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasInputType && entity.inputType.value == InputType.Observation && entity.hasDirection;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Direction);
        }
    }
}