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
    public class JoystickSystem : ReactiveSystem<GameEntity>
    {
        public JoystickSystem(Contexts contexts) : base(contexts.game)
        {

        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var joystick in entities)
            {
                if (joystick.hasTouchPosition)
                {
                    Vector3 dir = (joystick.touchPosition.value - joystick.center.value).normalized;
                    joystick.ReplaceDirection(dir);
                    var hat = joystick.GetHatEntity();
                    if (hat != null && hat.hasDistance && hat.hasLocalPosition)
                    {
                        hat.ReplaceLocalPosition(dir * hat.distance.value);
                    }
                }
                else
                {
                    var hat = joystick.GetHatEntity();
                    if (hat != null)
                    {
                        hat.ReplaceLocalPosition(Vector3.zero);
                    }
                    if (joystick.hasDirection)
                    {
                        joystick.RemoveDirection();
                    }
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isJoystick && entity.hasCenter;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TouchPosition.AddedOrRemoved());
        }
    }
}