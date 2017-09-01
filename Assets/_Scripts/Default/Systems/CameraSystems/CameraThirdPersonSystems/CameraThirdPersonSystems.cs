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
    public class CameraThirdPersonSystems : Feature
    {
        public CameraThirdPersonSystems(Contexts contexts) : base("Main Camera Third Person Systems")
        {
            Add(new CameraPivotFollowMainFigherSystem(contexts));
            Add(new CameraTrailCameraPivotSystem(contexts));
            Add(new CameraOrbitAroundCameraPivotSystem(contexts));
            if (GameConfig.instance.cameraAutoLock)
            {
                Add(new CameraAutoCheckSystem(contexts));
                Add(new CameraAutoLockSystem(contexts));
            }
            Add(new CameraLookCameraPivotSystem(contexts));
        }
    }
}