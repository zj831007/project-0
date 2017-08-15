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
            var camera = _context.game.cameraEntity;
            if (god != null && god.hasTransform && joystick != null && camera != null && camera.hasTransform)
            {
                var dir = joystick.leftJoystickDirection.value;
                dir.z = dir.y;
                dir.y = 0f;
                var rotation = Quaternion.Euler(0f, camera.transform.value.eulerAngles.y, 0f);
                god.transform.value.Translate(rotation * dir * god.god.speed);
            }
        }
    }
}