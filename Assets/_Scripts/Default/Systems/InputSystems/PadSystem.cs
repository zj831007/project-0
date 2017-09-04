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
    public class PadSystem : ReactiveSystem<InputEntity>
    {
        public PadSystem(Contexts contexts) : base(contexts.input)
        {

        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var pad in entities)
            {
                if (pad.hasPosition)
                {
                    if (pad.hasStep && pad.hasPreviousPosition)
                    {
                        if (pad.step.value == 5)
                        {
                            pad.ReplaceDirection(pad.position.value - pad.previousPosition.value);
                        }
                        else
                        {
                            if (pad.previousPosition.value != pad.position.value)
                            {
                                pad.ReplaceStep(pad.step.value + 1);
                            }
                        }
                    }
                    else
                    {
                        pad.ReplaceStep(0);
                    }
                    pad.ReplacePreviousPosition(pad.position.value);
                }
                else
                {
                    if (pad.hasDirection)
                    {
                        pad.RemoveDirection();
                    }
                    if (pad.hasStep)
                    {
                        pad.RemoveStep();
                    }
                }
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasName && entity.name.value.Contains("Pad");
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Position.AddedOrRemoved());
        }
    }
}