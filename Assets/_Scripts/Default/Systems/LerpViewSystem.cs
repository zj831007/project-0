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
    public class LerpViewSystem : IExecuteSystem
    {
        IGroup<GameEntity> _rigidbodyGroup;
        IGroup<GameEntity> _positionGroup;
        IGroup<GameEntity> _localPositionGroup;
        IGroup<GameEntity> _rotationGroup;

        public LerpViewSystem(Contexts contexts)
        {
            _rigidbodyGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Rigidbody, GameMatcher.Position));
            _positionGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LerpPostion, GameMatcher.Position));
            _localPositionGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LerpLocalPosition, GameMatcher.LocalPosition));
            _rotationGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LerpRotation, GameMatcher.Rotation));
        }

        public void Execute()
        {
            if (ProcessorManager.instance.physicsDirty)
            {
                foreach (var e in _rigidbodyGroup.GetEntities())
                {
                    var pos = e.rigidbody.value.position;
                    e.transform.value.position = e.position.value;
                    e.ReplacePosition(pos);
                }
            }
            foreach (var e in _positionGroup.GetEntities())
            {
                e.transform.value.position = Vector3.Lerp(e.transform.value.position, e.position.value, ProcessorManager.instance.timeInterpolation);
            }
            foreach (var e in _localPositionGroup.GetEntities())
            {
                e.transform.value.localPosition = Vector3.Lerp(e.transform.value.localPosition, e.localPosition.value, ProcessorManager.instance.timeInterpolation);
            }
            foreach (var e in _rotationGroup.GetEntities())
            {
                e.transform.value.rotation = Quaternion.Lerp(e.transform.value.rotation, e.rotation.value, ProcessorManager.instance.timeInterpolation);
            }
        }
    }
}