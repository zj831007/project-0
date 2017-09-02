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
        [SerializeField]
        GameConfigAsset _asset;

        public bool isGod
        {
            get { return _asset.IsGod; }
        }

        public float rightPadX
        {
            get { return _asset.RightPadX / 10f; }
        }

        public float rightPadY
        {
            get { return _asset.RightPadY / 10f; }
        }

        public float cameraDistance
        {
            get { return _asset.CameraDistance; }
        }

        public Vector3 cameraHeight
        {
            get { return new Vector3(0f, _asset.CameraHeight, 0f); }
        }

        public float cameraFlySpeed
        {
            get { return _asset.CameraFlySpeed / 20f; }
        }

        public float cameraWalkSpeed
        {
            get { return _asset.CameraWalkSpeed / 20f; }
        }

        public float cameraLiftSpeed
        {
            get { return _asset.CameraLiftSpeed / 20f; }
        }

        public float cameraUpDegree
        {
            get { return _asset.CameraUpDegree; }
        }

        public float cameraDownDegree
        {
            get { return _asset.CameraDownDegree; }
        }

        public bool cameraAutoLock
        {
            get { return _asset.CameraAutoLock; }
        }
        public bool cameraFastLock
        {
            get { return _asset.CameraFastLock; }
        }

        public float cameraAutoTime
        {
            get { return _asset.CameraAutoTime; }
        }

        public float cameraAutoDegree
        {
            get { return _asset.CameraAutoDegree; }
        }

        public float cameraAutoSpeed
        {
            get { return _asset.CameraAutoSpeed / 50f; }
        }

        public float fighterWalkSpeed
        {
            get { return _asset.FighterWalkSpeed / 20f; }
        }
    }
}