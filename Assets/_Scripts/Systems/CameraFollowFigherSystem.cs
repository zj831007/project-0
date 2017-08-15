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
    public class CameraFollowFigherSystem : ReactiveSystem<GameEntity>
    {
        Contexts _context;

        public CameraFollowFigherSystem(Contexts context) : base(context.game)
        {
            _context = context;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var camera = _context.game.cameraEntity;
            var fighter = _context.game.fighterEntity;
            if (camera != null && camera.hasTransform)
            {
                camera.AddParent(fighter.transform.value);
                var rotation = Quaternion.Euler(0f, camera.transform.value.eulerAngles.y, 0f);
                camera.AddLocalPosition(rotation * new Vector3(0, 0.1f, -0.5f));
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isFighter && entity.hasTransform;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Fighter);
        }
    }
}