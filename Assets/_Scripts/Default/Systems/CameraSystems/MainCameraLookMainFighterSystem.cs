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
    public class MainCameraLookMainFighterSystem : IExecuteSystem
    {
        GameContext _game;

        public MainCameraLookMainFighterSystem(Contexts context)
        {
            _game = context.game;
        }

        public void Execute()
        {
            var camera = _game.mainCameraEntity;
            var fighter = _game.mainFighterEntity;
            if (camera != null && camera.hasTransform && fighter != null && fighter.hasTransform)
            {
                var look = Quaternion.LookRotation(fighter.transform.value.position - camera.transform.value.position, Vector3.up);
                camera.transform.value.rotation = Quaternion.Slerp(camera.transform.value.rotation, look, 5 * Time.deltaTime);
                //camera.transform.value.LookAt(fighter.transform.value.position + GameConfig.instance.mainCameraHeightVector);
            }
        }
    }
}