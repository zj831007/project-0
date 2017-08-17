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
    public class MainCameraFollowMainFigherSystem : IExecuteSystem
    {
        GameContext _game;
        Vector3 _vel;

        public MainCameraFollowMainFigherSystem(Contexts context)
        {
            _game = context.game;
        }

        public void Execute()
        {
            var camera = _game.mainCameraEntity;
            var fighter = _game.mainFighterEntity;
            if (camera != null && camera.hasTransform && fighter != null && fighter.hasTransform)
            {
                var camTransform = camera.transform.value;
                camTransform.position = Vector3.SmoothDamp(camTransform.position, fighter.transform.value.position, ref _vel, 0.2f);
            }
        }
    }
}