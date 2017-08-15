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

namespace Project0.Gui
{
    public class LeftJoystick : Joystick
    {
        InputEntity _entity;

        protected override void Start()
        {
            base.Start();

            _entity = Contexts.sharedInstance.input.CreateEntity();
            _entity.AddLeftJoystickDirection(Vector3.zero);
        }

        protected override void OnTouching(Vector3 dir)
        {
            _entity.ReplaceLeftJoystickDirection(dir);
        }
        protected override void OnTouchEnd()
        {
            _entity.ReplaceLeftJoystickDirection(Vector3.zero);
        }
    }
}