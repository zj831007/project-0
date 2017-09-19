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
    public class TupleButtonSystem : ReactiveSystem<GameEntity>
    {
        public TupleButtonSystem(Contexts contexts) : base(contexts.game)
        {

        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var tupleButton in entities)
            {
                if (tupleButton.hasTouchPosition)
                {
                    var pos = tupleButton.touchPosition.value;
                    var center = tupleButton.center.value;
                    Vector3 dir;
                    if (tupleButton.isHorizontal)
                    {
                        if (pos.x > center.x)
                        {
                            dir = new Vector3(1f, 0f, 0f);
                        }
                        else
                        {
                            dir = new Vector3(-1f, 0f, 0f);
                        }
                    }
                    else
                    {
                        if (pos.y > center.y)
                        {
                            dir = new Vector3(0f, 1f, 0f);
                        }
                        else
                        {
                            dir = new Vector3(0f, -1f, 0f);
                        }
                    }
                    tupleButton.ReplaceDirection(dir);
                }
                else
                {
                    if (tupleButton.hasDirection)
                    {
                        tupleButton.RemoveDirection();
                    }
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isTupleButton && entity.hasCenter;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TouchPosition.AddedOrRemoved());
        }
    }
}