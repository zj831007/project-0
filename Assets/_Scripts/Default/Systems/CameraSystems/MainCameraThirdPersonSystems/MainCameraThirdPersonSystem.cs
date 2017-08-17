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
    public class MainCameraThirdPersonSystem : Feature
    {
        public MainCameraThirdPersonSystem(Contexts contexts) : base("Main Camera Third Person Systems")
        {
            Add(new MainCameraPivotFollowMainFigherSystem(contexts));
            Add(new MainCameraOrbitAroundMainCameraPivotSystem(contexts));
            Add(new MainCameraTrailMainCameraPivotSystem(contexts));
            Add(new MainCameraLookMainCameraPivotSystem(contexts));
        }
    }
}