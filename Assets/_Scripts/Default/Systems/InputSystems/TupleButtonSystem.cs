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
    public class TupleButtonSystem : ReactiveSystem<InputEntity>
    {
        public TupleButtonSystem(Contexts contexts) : base(contexts.input)
        {

        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var tupleButton in entities)
            {
                if (tupleButton.hasPosition)
                {
                    var pos = tupleButton.position.value;
                    var center = tupleButton.center.value;
                    Vector3 dir;
                    if (tupleButton.horizontal.value)
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

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasCenter && entity.hasHorizontal && entity.hasName 
                && entity.name.value.Contains("TupleButton");
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Position.AddedOrRemoved());
        }
    }
}