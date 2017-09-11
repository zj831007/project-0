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
        GameContext _game;

        public CameraLiftSystem(Contexts context) : base(context.input)
        {
            _game = context.game;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var tuple = entities[0];
            var player = _game.playerEntity;
            if (player != null && player.hasCameraTransform)
            {
                player.cameraTransform.value.Translate(tuple.direction.value * GameConfig.instance.cameraLiftSpeed * Time.deltaTime);
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