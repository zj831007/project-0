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
    public class CameraOrbitAroundCameraPivotSystem : ReactiveSystem<GameEntity>
    {
        public CameraOrbitAroundCameraPivotSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var pad in entities)
            {
                var camera = pad.GetCameraEntity();
                if (camera != null && camera.hasDistance && camera.hasDirection)
                {
                    var dir = pad.direction.value;
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        dir.y = 0f;
                    }
                    var pivot = camera.GetPivotEntity();
                    if (pivot != null)
                    {
                        var x = dir.x * GameConfig.instance.rightPadX;
                        var y = dir.y * GameConfig.instance.rightPadY;
                        var toPivot = camera.direction.value;
                        var angle = -toPivot.AngleFromXZ();
                        var min = GameConfig.instance.cameraDownDegree;
                        var max = GameConfig.instance.cameraUpDegree;
                        if (camera.hasAngleOffset)
                        {
                            min = Mathf.Max(-89f, min - camera.angleOffset.value);
                            //max = Mathf.Min(89f, max - camera.angleOffset.value);
                        }
                        y = Math.Min(angle - min, Mathf.Max(angle - max, y));
                        toPivot = Quaternion.AngleAxis(x, Vector3.up) * toPivot;
                        toPivot = Quaternion.AngleAxis(y, Vector3.Cross(toPivot, Vector3.up)) * toPivot;
                        var position = pivot.position.value - toPivot * camera.distance.value;
                        camera.ReplacePosition(position);
                        camera.ReplaceDirection(toPivot);
                    }
                }
            }

        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasDirection && entity.hasInputType && entity.inputType.value == InputType.Observation;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Direction);
        }
    }
}