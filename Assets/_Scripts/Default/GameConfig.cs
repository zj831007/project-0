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
using Project0.Design;

namespace Project0
{
    public class GameConfig : SceneSingleton<GameConfig>
    {
        public float rightPadSensitivityX = 50f;
        public float rightPadSensitivityY = 50f;
        public float mainCameraDistance = 1f;
        public float mainCameraDistanceSquare
        {
            get { return mainCameraDistance * mainCameraDistance; }
        }
        public Vector3 mainCameraDistanceVector
        {
            get { return new Vector3(0f, 0f, mainCameraDistance); }
        }
        public float mainCameraHeight = 1f;
        public Vector3 mainCameraHeightVector
        {
            get { return new Vector3(0f, mainCameraHeight, 0f); }
        }
        [Range(0, 100)]
        public float fighterFlySpeedPercent = 50f;
        public float fighterFlySpeed
        {
            get { return fighterFlySpeedPercent / 20f; }
        }
        [Range(0, 100)]
        public float fighterMoveSpeedPercent = 50f;
        public float fighterMoveSpeed
        {
            get { return fighterMoveSpeedPercent / 20f; }
        }
        [Range(0, 100)]
        public float fighterLiftSpeedPercent = 50f;
        public float fighterLiftSpeed
        {
            get { return fighterLiftSpeedPercent / 20f; }
        }
    }
}