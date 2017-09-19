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
    public class ThirdPersonCameraProcessor : Processor
    {
        public static GameEntity camera { get; private set; }
        public static IGroup<GameEntity> cameras { get; private set; }

        public ThirdPersonCameraProcessor(Contexts contexts) : base("Third Person Camera Processor")
        {
            cameras = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Camera, GameMatcher.Direction, GameMatcher.Position, GameMatcher.Distance));
            Add(new PivotFollowTargetSystem(contexts));
            Add(new CameraTerrainSystem(contexts));
            Add(new CameraTrailPivotSystem(contexts));
            Add(new CameraOrbitAroundCameraPivotSystem(contexts));
            Add(new CameraAutoCheckSystem(contexts));
            Add(new CameraAutoLockSystem(contexts));
            Add(new PreventCameraBlockSystem(contexts));
            Add(new CameraLookPivotSystem(contexts));
            //if (GameConfig.instance.cameraAutoLock == false)
            //{
            //    DeactivateExecuteSystem(nameof(CameraAutoCheckSystem));
            //    DeactivateExecuteSystem(nameof(CameraAutoLockSystem));
            //}
        }
    }
}