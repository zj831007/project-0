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
        Contexts _context;
        Vector3 _vel;

        public MainCameraFollowMainFigherSystem(Contexts context)
        {
            _context = context;
        }

        public void Execute()
        {
            var camera = _context.game.mainCameraEntity;
            var fighter = _context.game.mainFighterEntity;
            if (camera != null && camera.hasTransform && fighter != null && fighter.hasTransform)
            {
                var camTransform = camera.transform.value;
                var fighterTransform = fighter.transform.value;
                var focus = fighterTransform.position + GameConfig.instance.mainCameraHeightVector;
                var toCamera = camTransform.position - focus;
                var dest = fighterTransform.position + toCamera.normalized * GameConfig.instance.mainCameraDistance;
                //camTransform.position = Vector3.SmoothDamp(camTransform.position, dest, ref _vel, 0.2f);

                //camTransform.RotateAround(fighterTransform.position, Vector3.up, 5 * Time.deltaTime);
                //camTransform.position = Vector3.Slerp(camTransform.position, dest, 5 * Time.deltaTime);
            }
        }
    }
}