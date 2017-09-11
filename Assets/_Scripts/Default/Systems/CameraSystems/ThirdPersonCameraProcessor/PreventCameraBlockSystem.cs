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
    public class PreventCameraBlockSystem : IExecuteSystem
    {
        GameContext _game;
        float _awayVel;
        float _nearVel;

        public PreventCameraBlockSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var player = _game.playerEntity;
            var camera = ThirdPersonCameraProcessor.camera;
            var pivotTransform = camera.pivotTransform.value;
            var camTransform = camera.transform.value;
            RaycastHit hit;
            var mask = GameConfig.instance.cameraBlockMask & ~GameConfig.instance.playerMask;
            var dir = camera.direction.value;
            float newDis;
            if (Physics.Raycast(pivotTransform.position, dir, out hit, camera.distance.value, mask))
            {
                newDis = Mathf.Max(Mathf.SmoothDamp(camera.distance.value, GameConfig.instance.cameraMinDistance, ref _nearVel, 0.2f), Vector3.Distance(pivotTransform.position, hit.point));
            }
            else
            {
                if (Physics.Raycast(pivotTransform.position, dir, out hit, GameConfig.instance.cameraMaxDistance, mask))
                {
                    newDis = Vector3.Distance(pivotTransform.position, hit.point);
                }
                else
                {
                    newDis = Mathf.SmoothDamp(camera.distance.value, GameConfig.instance.cameraMaxDistance, ref _awayVel, 0.2f);
                }
            }
            camTransform.position = pivotTransform.position + camera.direction.value * newDis;
            camera.ReplaceDistance(newDis);
        }
    }
}