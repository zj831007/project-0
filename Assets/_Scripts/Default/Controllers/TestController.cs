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
    public class TestController : Controller
    {
        protected override Systems GetSystems()
        {
            var contexts = Contexts.sharedInstance;
            switch (GameConfig.instance.inputMode)
            {
                case InputMode.God:
                    return new Feature("Systems")
                        //.Add(new TransformSystems(contexts))
                        .Add(new InitInputActivitySystem())
                        .Add(new MainCameraFlySystem(contexts))
                        .Add(new MainCameraWalkSystem(contexts))
                        .Add(new MainCameraLiftSystem(contexts))
                        .Add(new MainCameraFreeRotationSystem(contexts));
                default:
                    return new Feature("Systems")
                        .Add(new InitInputActivitySystem())
                        //.Add(new TransformSystems(contexts))
                        .Add(new MainFighterWalkSystem(contexts))
                        .Add(new MainCameraThirdPersonSystem(contexts));
            }
        }
    }
}