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
    [Game]
    public class LocalPositionSystem : ReactiveSystem<GameEntity>
    {
        GameContext _game;

        public LocalPositionSystem(Contexts contexts) : base(contexts.game)
        {
            _game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                e.transform.value.localPosition = e.localPosition.value;
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTransform && entity.hasLocalPosition;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LocalPosition);
        }
    }
}