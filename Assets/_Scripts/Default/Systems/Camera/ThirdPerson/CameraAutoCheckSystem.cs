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
    public class CameraAutoCheckSystem : ReactiveSystem<GameEntity>
    {
        public static bool autoLock { get; private set; }

        bool _canLock = true;//pass
        float _time;//pass

        public CameraAutoCheckSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                switch (entity.inputType.value)
                {
                    case InputType.TerrainMovement:
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
                    case InputType.Observation:
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

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasInputType;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Direction.AddedOrRemoved());
        }
    }
}