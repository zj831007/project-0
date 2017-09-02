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
        protected override Processor GetProcessor()
        {
            var contexts = Contexts.sharedInstance;
            Processor processor = new Processor("Game Processor");
            processor
                .Add(new InitInputSystem())
                .Add(new MainFighterWalkSystem(contexts))
                .Add(new ThirdPersonCameraProcessor(contexts))
                .Add(new CameraFlySystem(contexts))
                .Add(new CameraWalkSystem(contexts))
                .Add(new CameraLiftSystem(contexts))
                .Add(new CameraFreeRotationSystem(contexts))
                .Add(new DestroyTransformSystem(contexts))
                .Add(new DestroyEntitySystem(contexts));

            if (GameConfig.instance.isGod)
            {
                processor
                    .DeactivateExecuteSystem("MainFighterWalkSystem")
                    .DeactivateExecuteSystem("ThirdPersonCameraProcessor");
            }
            else
            {
                processor
                    .DeactivateExecuteSystem("CameraFlySystem")
                    .DeactivateExecuteSystem("CameraWalkSystem")
                    .DeactivateExecuteSystem("CameraLiftSystem")
                    .DeactivateExecuteSystem("CameraFreeRotationSystem");
            }
            return processor;
        }
    }
}