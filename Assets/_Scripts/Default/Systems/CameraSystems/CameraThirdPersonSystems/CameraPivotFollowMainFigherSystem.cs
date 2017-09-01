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
    public class CameraPivotFollowMainFigherSystem : IExecuteSystem, IInitializeSystem
    {
        GameContext _game;
        Vector3 _vel;

        public CameraPivotFollowMainFigherSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var camera = _game.cameraEntity;
            if (camera != null && camera.hasPivot)
            {
                var pivot = camera.pivot.value;
                var fighter = _game.mainFighterEntity;
                if (pivot.hasTransform && fighter != null && fighter.hasTransform)
                {
                    var pivotTransform = pivot.transform.value;
                    pivotTransform.position = Vector3.SmoothDamp(pivotTransform.position, fighter.transform.value.position + GameConfig.instance.cameraHeight, ref _vel, 0.2f);
                }
            }
        }

        public void Initialize()
        {
            if(GameConfig.instance.inputMode == InputMode.Normal)
            {
                var camera = _game.cameraEntity;
                var fighter = _game.mainFighterEntity;
                if (camera != null && fighter != null && fighter.hasTransform)
                {
                    var pivot = Contexts.sharedInstance.game.CreateEntity();
                    var pivotTransform = new GameObject("CameraPivot").transform;
                    pivotTransform.position = fighter.transform.value.position + GameConfig.instance.cameraHeight;
                    pivot.AddTransform(pivotTransform);
                    camera.AddPivot(pivot);
                }
            }
        }
    }
}