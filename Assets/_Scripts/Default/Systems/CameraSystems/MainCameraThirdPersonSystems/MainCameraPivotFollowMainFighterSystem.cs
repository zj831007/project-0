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
    public class MainCameraPivotFollowMainFigherSystem : IExecuteSystem
    {
        GameContext _game;
        Vector3 _vel;

        public MainCameraPivotFollowMainFigherSystem(Contexts context)
        {
            _game = context.game;
        }

        public void Execute()
        {
            var camera = _game.mainCameraEntity;
            if (camera != null && camera.hasPivot)
            {
                var pivot = camera.pivot.value;
                var fighter = _game.mainFighterEntity;
                if (pivot.hasTransform && fighter != null && fighter.hasTransform)
                {
                    var pivotTransform = pivot.transform.value;
                    pivotTransform.position = Vector3.SmoothDamp(pivotTransform.position, fighter.transform.value.position + GameConfig.instance.mainCameraHeightVector, ref _vel, 0.2f);
                }
            }
        }
    }
}