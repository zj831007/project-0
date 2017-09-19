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
        float _awayVel;//pass
        float _nearVel;//pass

        public PreventCameraBlockSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            //foreach (var camera in ThirdPersonCameraProcessor.cameras.GetEntities())
            //{
            //    var pivot = camera.GetPivotEntity();
            //    if (pivot != null && pivot.hasPosition)
            //    {
            //        RaycastHit hit;
            //        var mask = GameConfig.instance.cameraBlockMask & ~GameConfig.instance.pawnMask;
            //        var dir = camera.direction.value;
            //        float newDis;
            //        if (Physics.Raycast(pivot.position.value, dir, out hit, camera.distance.value, mask))
            //        {
            //            newDis = Mathf.Max(Mathf.SmoothDamp(camera.distance.value, GameConfig.instance.cameraMinDistance, ref _nearVel, 0.2f), Vector3.Distance(pivot.position.value, hit.point));
            //        }
            //        else
            //        {
            //            if (Physics.Raycast(pivot.position.value, dir, out hit, GameConfig.instance.cameraMaxDistance, mask))
            //            {
            //                newDis = Vector3.Distance(pivot.position.value, hit.point);
            //            }
            //            else
            //            {
            //                newDis = Mathf.SmoothDamp(camera.distance.value, GameConfig.instance.cameraMaxDistance, ref _awayVel, 0.2f);
            //            }
            //        }
            //        camera.ReplacePosition(pivot.position.value + camera.direction.value * newDis);
            //        camera.ReplaceDistance(newDis);
            //    }
            //}
        }
    }
}