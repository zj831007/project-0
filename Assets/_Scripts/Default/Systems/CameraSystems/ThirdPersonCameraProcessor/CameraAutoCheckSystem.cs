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
    public class CameraAutoCheckSystem : ReactiveSystem<InputEntity>
    {
        public static bool autoLock { get; private set; }

        GameContext _game;
        bool _canLock = true;
        float _time;

        public CameraAutoCheckSystem(Contexts contexts) : base(contexts.input)
        {
            _game = contexts.game;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                switch (entity.name.value)
                {
                    case "LeftJoystick":
                        if (entity.hasDirection && _canLock)
                        {
                            if (GameConfig.instance.cameraFastLock)
                            {
                                autoLock = true;
                            }
                            else
                            {
                                if (_time == 0f)
                                {
                                    _time = Time.time;
                                }
                                else if (Time.time - _time >= GameConfig.instance.cameraAutoTime)
                                {
                                    autoLock = true;
                                }
                            }
                        }
                        else
                        {
                            _time = 0f;
                        }
                        break;
                    case "RightPad":
                        if (entity.hasDirection)
                        {
                            autoLock = false;
                            _canLock = false;
                            _time = 0f;
                        }
                        else
                        {
                            _canLock = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasName;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Direction.AddedOrRemoved());
        }
    }
}