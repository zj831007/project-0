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
    public class JoystickSystem : ReactiveSystem<InputEntity>
    {
        public JoystickSystem(Contexts contexts) : base(contexts.input)
        {

        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var joystick in entities)
            {
                var hat = joystick.hat.value;
                if (hat.hasTransform && hat.hasCenter && hat.hasDistance)
                {
                    if (joystick.hasPosition)
                    {
                        Vector3 dir = (joystick.position.value - hat.center.value).normalized;
                        hat.transform.value.localPosition = dir * hat.distance.value;
                        joystick.ReplaceDirection(dir);
                    }
                    else
                    {
                        hat.transform.value.localPosition = Vector3.zero;
                        if (joystick.hasDirection)
                        {
                            joystick.RemoveDirection();
                        }
                    }
                }
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasHat && entity.hasName && entity.name.value.Contains("Joystick");
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Position.AddedOrRemoved());
        }
    }
}