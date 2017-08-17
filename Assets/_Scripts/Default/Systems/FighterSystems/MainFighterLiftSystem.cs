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
    public class MainFighterLiftSystem : ReactiveSystem<InputEntity>
    {
        Contexts _context;

        public MainFighterLiftSystem(Contexts context) : base(context.input)
        {
            _context = context;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var tuple = entities[0];
            var fighter = _context.game.mainFighterEntity;
            if (fighter != null && fighter.hasTransform)
            {
                var dir = new Vector3(0, 1, 0) * tuple.tupleButtonDirection.value;
                fighter.transform.value.Translate(dir * GameConfig.instance.fighterLiftSpeed * Time.deltaTime);
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasTupleButtonDirection && entity.hasName && entity.name.value == "Left";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.TupleButtonDirection);
        }
    }
}