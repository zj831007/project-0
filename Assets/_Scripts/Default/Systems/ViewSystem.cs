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
    public class ViewSystem : IExecuteSystem
    {
        IGroup<GameEntity> _positionEntities;
        IGroup<GameEntity> _localPositionEntities;
        IGroup<GameEntity> _rotationEntities;

        public ViewSystem(Contexts contexts)
        {
            _positionEntities = contexts.game.GetGroup(GameMatcher.Position);
            _localPositionEntities = contexts.game.GetGroup(GameMatcher.LocalPosition);
            _rotationEntities = contexts.game.GetGroup(GameMatcher.Rotation);
        }

        public void Execute()
        {
            foreach (var e in _positionEntities.GetEntities())
            {
                e.transform.value.position = e.position.value;
            }
            foreach (var e in _localPositionEntities.GetEntities())
            {
                e.transform.value.localPosition = e.localPosition.value;
            }
            foreach (var e in _rotationEntities.GetEntities())
            {
                e.transform.value.rotation = e.rotation.value;
            }
        }
    }
}