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
    public class TestManager : Controller
    {
        protected override Processor GetFixedProcessor()
        {
            var contexts = Contexts.sharedInstance;
            Processor processor = new Processor("Fixed Game Processor");
            processor.Add(new PlayerRunSystem(contexts));
            return processor;
        }

        protected override Processor GetProcessor()
        {
            var contexts = Contexts.sharedInstance;
            Processor processor = new Processor("Game Processor");
            processor
                .Add(new InitInputSystem())
                .Add(new TouchPositionSystem(contexts))
                .Add(new JoystickSystem(contexts))
                .Add(new TupleButtonSystem(contexts))
                .Add(new PadSystem(contexts))
                //.Add(new PlayerRunSystem(contexts))
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
                    //.DeactivateExecuteSystem(nameof(PlayerRunSystem))
                    .DeactivateExecuteSystem(nameof(ThirdPersonCameraProcessor));
            }
            else
            {
                processor
                    .DeactivateExecuteSystem(nameof(CameraFlySystem))
                    .DeactivateExecuteSystem(nameof(CameraWalkSystem))
                    .DeactivateExecuteSystem(nameof(CameraLiftSystem))
                    .DeactivateExecuteSystem(nameof(CameraFreeRotationSystem));
            }
            return processor;
        }
    }
}