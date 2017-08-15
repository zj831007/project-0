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
    public class RightJoystick : Joystick
    {
        InputEntity _entity;

        protected override void Start()
        {
            base.Start();

            _entity = Contexts.sharedInstance.input.CreateEntity();
            _entity.AddRightJoystickDirection(Vector3.zero);
        }

        protected override void OnTouching(Vector3 dir)
        {
            _entity.ReplaceRightJoystickDirection(dir);
        }
        protected override void OnTouchEnd()
        {
            _entity.ReplaceRightJoystickDirection(Vector3.zero);
        }

    }
}