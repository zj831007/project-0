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
    public class TouchPositionSystem : IExecuteSystem
    {
        IGroup<GameEntity> _idEntityGroup;

        public TouchPositionSystem(Contexts contexts)
        {
            _idEntityGroup = contexts.game.GetGroup(GameMatcher.ID);
        }

        public void Execute()
        {
            var touches = Input.touches;
            foreach (var e in _idEntityGroup.GetEntities())
            {
#if UNITY_EDITOR
                e.ReplaceTouchPosition(Input.mousePosition);
#else
                foreach (var touch in touches)
                {
                    if (e.iD.value == touch.fingerId)
                    {
                        e.ReplaceTouchPosition(touch.position);
                        break;
                    }
                }
#endif
            }
        }
    }
}