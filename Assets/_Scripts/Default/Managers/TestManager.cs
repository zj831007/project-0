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
    public class TestManager : ProcessorManager
    {
        public override InputMode inputMode
        {
            get { return _mode; }
            set
            {
                switch (_mode)
                {
                    case InputMode.God:
                        _processor
                            .DeactivateExecuteSystem(nameof(CameraFlySystem))
                            .DeactivateExecuteSystem(nameof(CameraWalkSystem))
                            .DeactivateExecuteSystem(nameof(CameraLiftSystem))
                            .DeactivateExecuteSystem(nameof(CameraFreeRotationSystem));
                        break;
                    case InputMode.ThirdPerson:
                        _processor
                            .DeactivateExecuteSystem(nameof(PawnRunSystem))
                            .DeactivateExecuteSystem(nameof(ThirdPersonCameraProcessor));
                        break;
                    default:
                        break;
                }
                switch (value)
                {
                    case InputMode.God:
                        _processor
                            .ActivateExecuteSystem(nameof(CameraFlySystem))
                            .ActivateExecuteSystem(nameof(CameraWalkSystem))
                            .ActivateExecuteSystem(nameof(CameraLiftSystem))
                            .ActivateExecuteSystem(nameof(CameraFreeRotationSystem));
                        break;
                    case InputMode.ThirdPerson:
                        _processor
                            .ActivateExecuteSystem(nameof(PawnRunSystem))
                            .ActivateExecuteSystem(nameof(ThirdPersonCameraProcessor));
                        break;
                    default:
                        break;
                }
                _mode = value;
            }
        }

        public override bool autoLock
        {
            get { return _lock; }
            set
            {
                var proc = _processor[nameof(ThirdPersonCameraProcessor)];
                if (value)
                {
                    proc
                        .ActivateExecuteSystem(nameof(CameraAutoCheckSystem))
                        .ActivateExecuteSystem(nameof(CameraAutoLockSystem));
                }
                else
                {
                    proc
                        .DeactivateExecuteSystem(nameof(CameraAutoCheckSystem))
                        .DeactivateExecuteSystem(nameof(CameraAutoLockSystem));
                }
                _lock = value;
            }
        }
        protected override Processor processor { get { return _processor; } }
        protected override Processor viewProcessor { get { return _viewProcessor; } }

        InputMode _mode;
        bool _lock;
        Processor _processor;
        Processor _viewProcessor;
        private void Start()
        {
            var contexts = Contexts.sharedInstance;
            _processor = new Processor(nameof(processor));
            _processor
                .Add(new GameInitSystem(contexts))
                .Add(new InactiveSyshtem(contexts))
                .Add(new ViewSystem(contexts))
                .Add(new TouchPositionSystem(contexts))
                .Add(new JoystickSystem(contexts))
                .Add(new TupleButtonSystem(contexts))
                .Add(new PadSystem(contexts))
                .Add(new CameraFlySystem(contexts))
                .Add(new CameraWalkSystem(contexts))
                .Add(new CameraLiftSystem(contexts))
                .Add(new CameraFreeRotationSystem(contexts))
                .Add(new PawnRunSystem(contexts))
                .Add(new ThirdPersonCameraProcessor(contexts))
                .Add(new DestroyTransformSystem(contexts))
                .Add(new DestroyEntitySystem(contexts));
            _viewProcessor = new Processor(nameof(viewProcessor));
            _viewProcessor
                .Add(new LerpViewSystem(contexts));
            processor.Initialize();
            viewProcessor.Initialize();
            inputMode = GameConfig.instance.inputMode;
            autoLock = GameConfig.instance.cameraAutoLock;
        }
    }
}