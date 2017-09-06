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
    public class DestroyTransformSystem : ReactiveSystem<GameEntity>
    {
        public DestroyTransformSystem(Contexts contexts) : base(contexts.game)
        {

        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                Object.Destroy(entity.transform.value.gameObject);
                entity.RemoveTransform();
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDestroyed && entity.hasTransform;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Destroyed, GameMatcher.Transform));
        }
    }
}