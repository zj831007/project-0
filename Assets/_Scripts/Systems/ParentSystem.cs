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
    public class ParentSystem : ReactiveSystem<GameEntity>
    {
        Contexts _context;

        public ParentSystem(Contexts context) : base(context.game)
        {
            _context = context;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                if (e.hasParent)
                {
                    e.transform.value.SetParent(e.parent.value);
                }
                else
                {
                    e.transform.value.SetParent(null);
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTransform;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Parent);
        }
    }
}