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
    public class PivotFollowTargetSystem : IExecuteSystem
    {
        IGroup<GameEntity> _pivots;

        public PivotFollowTargetSystem(Contexts contexts)
        {
            _pivots = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Pivot, GameMatcher.Velocity));
        }

        public void Execute()
        {
            foreach (var p in _pivots.GetEntities())
            {
                var target = p.GetTargetEntity();
                if (target != null && target.hasPosition)
                {
                    Vector3 vel = p.velocity.value;
                    p.ReplacePosition(Vector3.SmoothDamp(p.position.value, target.position.value + GameConfig.instance.cameraHeightVector, ref vel, 0.2f));
                    p.ReplaceVelocity(vel);
                }
            }
        }
    }
}