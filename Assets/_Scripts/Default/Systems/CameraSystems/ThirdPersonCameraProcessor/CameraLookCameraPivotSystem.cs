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
    public class CameraLookCameraPivotSystem : IExecuteSystem
    {
        GameContext _game;

        public CameraLookCameraPivotSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var camera = _game.cameraEntity;
            var fighter = _game.mainFighterEntity;
            if (camera != null && camera.hasPivot && fighter != null && fighter.hasTransform)
            {
                var pivot = camera.pivot.value;
                if (pivot.hasTransform)
                {
                    camera.transform.value.LookAt(pivot.transform.value.position);
                }

            }
        }
    }
}