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
        IGroup<InputEntity> _idEntityGroup;

        public TouchPositionSystem(Contexts contexts)
        {
            _idEntityGroup = contexts.input.GetGroup(InputMatcher.ID);
        }

        public void Execute()
        {
            var touches = Input.touches;
            foreach (var e in _idEntityGroup.GetEntities())
            {
#if UNITY_EDITOR
                e.ReplacePosition(Input.mousePosition);
#else
                foreach (var touch in touches)
                {
                    if (e.iD.value == touch.fingerId)
                    {
                        e.ReplacePosition(touch.position);
                        break;
                    }
                }
#endif
            }
        }
    }
}