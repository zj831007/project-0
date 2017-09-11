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
        public static GameEntity camera { get; private set; }
        GameContext _game;

        public ThirdPersonCameraProcessor(Contexts contexts) : base("Third Person Camera Processor")
        {
            _game = contexts.game;
            Add(new PivotFollowPlayerSystem(contexts));
            Add(new CameraTerrainSystem(contexts));
            Add(new CameraTrailPivotSystem(contexts));
            Add(new CameraOrbitAroundCameraPivotSystem(contexts));
            Add(new CameraAutoCheckSystem(contexts));
            Add(new CameraAutoLockSystem(contexts));
            Add(new PreventCameraBlockSystem(contexts));
            Add(new CameraLookPivotSystem(contexts));
            if (GameConfig.instance.cameraAutoLock == false)
            {
                DeactivateExecuteSystem(nameof(CameraAutoCheckSystem));
                DeactivateExecuteSystem(nameof(CameraAutoLockSystem));
            }
        }
        public override void Initialize()
        {
            var player = _game.playerEntity;
            if (player != null && player.hasCameraTransform)
            {
                camera = player.cameraTransform.value.getGameEntity();
                if (camera != null)
                {
                    camera.AddDistance(GameConfig.instance.cameraMaxDistance);
                    if (camera.hasPivotTransform)
                    {
                        var dir = (camera.transform.value.position - camera.pivotTransform.value.position).normalized;
                        camera.AddDirection(dir);
                    }
                    else
                    {
                        camera.AddDirection(Vector3.back);
                    }
                    base.Initialize();
                }
            }
        }
        public override void Execute()
        {
            var player = _game.playerEntity;
            if (player != null && player.hasCameraTransform)
            {
                camera = player.cameraTransform.value.getGameEntity();
                if (camera != null && camera.hasPivotTransform && camera.hasDistance && camera.hasDirection)
                {
                    base.Execute();
                }
            }
        }
        public override void TearDown()
        {
            var player = _game.playerEntity;
            if (player != null && player.hasCameraTransform)
            {
                var camera = player.cameraTransform.value.getGameEntity();
                if (camera != null)
                {
                    base.TearDown();
                    if (camera.hasDistance)
                    {
                        camera.RemoveDistance();
                    }
                    if (camera.hasDirection)
                    {
                        camera.RemoveDirection();
                    }
                }
            }
        }
    }
}