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
    public class GodMovementSystem : IExecuteSystem
    {
        Contexts _context;

        public GodMovementSystem(Contexts context)
        {
            _context = context;
        }

        public void Execute()
        {
            var god = _context.game.godEntity;
            var joystick = _context.input.leftJoystickDirectionEntity;
            if (god != null && god.hasTransform && joystick != null)
            {
                var dir = joystick.leftJoystickDirection.value;
                god.transform.value.Translate(new Vector3(dir.x, 0, dir.y) * god.god.speed);
            }
        }
    }
}