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
    public class PadSystem : ReactiveSystem<GameEntity>
    {
        public PadSystem(Contexts contexts) : base(contexts.game)
        {

        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var pad in entities)
            {
                if (pad.hasTouchPosition)
                {
                    if (pad.hasPreviousTouchPosition)
                    {
                        pad.ReplaceDirection(pad.touchPosition.value - pad.previousTouchPosition.value);
                    }
                    pad.ReplacePreviousTouchPosition(pad.touchPosition.value);
                }
                else
                {
                    if (pad.hasDirection)
                    {
                        pad.RemoveDirection();
                    }
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPad;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TouchPosition.AddedOrRemoved());
        }
    }
}