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
    public class ThirdPersonCameraProcessor : Processor, IInitializeSystem, ITearDownSystem
    {
        GameContext _game;

        public ThirdPersonCameraProcessor(Contexts contexts) : base("Third Person Camera Processor")
        {
            _game = contexts.game;
            Add(new CameraPivotFollowMainFigherSystem(contexts));
            Add(new CameraTerrainSystem(contexts));
            Add(new CameraTrailCameraPivotSystem(contexts));
            Add(new CameraOrbitAroundCameraPivotSystem(contexts));
            Add(new CameraAutoCheckSystem(contexts));
            Add(new CameraAutoLockSystem(contexts));
            Add(new PreventCameraBlockSystem(contexts));
            Add(new CameraLookCameraPivotSystem(contexts));
            if (GameConfig.instance.cameraAutoLock == false)
            {
                DeactivateExecuteSystem("CameraAutoCheckSystem");
                DeactivateExecuteSystem("CameraAutoLockSystem");
            }
        }
        public override void Initialize()
        {
            base.Initialize();
            var camera = _game.cameraEntity;
            if (camera != null)
            {
                camera.AddDistance(GameConfig.instance.cameraMaxDistance);
                if (camera.hasPivot)
                {
                    var pivot = camera.pivot.value;
                    if (pivot.hasTransform)
                    {
                        var dir = (camera.transform.value.position - pivot.transform.value.position).normalized;
                        camera.AddDirection(dir);
                    }
                }
            }
        }
        public override void TearDown()
        {
            base.TearDown();
            var camera = _game.cameraEntity;
            if (camera != null)
            {
                if (camera.hasDistance)
                {
                    camera.RemoveDistance();
                }
                if (camera.hasDistance)
                {
                    camera.RemoveDirection();
                }
            }
        }
    }
}