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
        public object this[string field]
        {
            get { return GetType().GetProperty(field).GetValue(this); }
            set { GetType().GetProperty(field).SetValue(this, value); }
        }

        //static GameConfigAsset _staticAsset;
        [SerializeField]
        GameConfigAsset _asset;
        public InputMode inputMode
        {
            get { return _asset.input.mode; }
            set
            {
                if (Application.isPlaying)
                {
                    InputLayout.inputMode = value;
                    ProcessorManager.instance.inputMode = value;
                }
                _asset.input.mode = value;
            }
        }

        public float rightPadX
        {
            get { return _asset.input.rightPadX / 100f; }
            set { _asset.input.rightPadX = value; }
        }

        public float rightPadY
        {
            get { return _asset.input.rightPadY / 100f; }
            set { _asset.input.rightPadY = value; }
        }

        public LayerMask cameraBlockMask
        {
            get { return _asset.camera.blockMask; }
            set { _asset.camera.blockMask = value; }
        }

        public float cameraMaxDistance
        {
            get { return _asset.camera.maxDistance; }
            set { _asset.camera.maxDistance = value; }
        }

        public float cameraMinDistance
        {
            get { return _asset.camera.minDistance; }
            set { _asset.camera.minDistance = value; }
        }

        public Vector3 cameraHeightVector
        {
            get { return new Vector3(0f, _asset.camera.height, 0f); }
        }

        public float cameraHeight
        {
            get { return _asset.camera.height; }
            set { _asset.camera.height = value; }
        }

        public float cameraFlySpeed
        {
            get { return _asset.camera.flySpeed / 100f; }
            set { _asset.camera.flySpeed = value; }
        }

        public float cameraWalkSpeed
        {
            get { return _asset.camera.walkSpeed / 100f; }
            set { _asset.camera.walkSpeed = value; }
        }

        public float cameraLiftSpeed
        {
            get { return _asset.camera.liftSpeed / 100f; }
            set { _asset.camera.liftSpeed = value; }
        }

        public float cameraUpDegree
        {
            get { return _asset.camera.upDegree; }
            set { _asset.camera.upDegree = value; }
        }

        public float cameraDownDegree
        {
            get { return _asset.camera.downDegree; }
            set { _asset.camera.downDegree = value; }
        }

        public bool cameraAutoLock
        {
            get { return _asset.camera.autoLock; }
            set
            {
                if (Application.isPlaying)
                {
                    ProcessorManager.instance.autoLock = value;
                }
                _asset.camera.autoLock = value;
            }
        }
        public bool cameraFastLock
        {
            get { return _asset.camera.fastLock; }
            set { _asset.camera.fastLock = value; }
        }

        public float cameraAutoTime
        {
            get { return _asset.camera.autoTime; }
            set { _asset.camera.autoTime = value; }
        }

        public float cameraAutoDegree
        {
            get { return _asset.camera.autoDegree; }
            set { _asset.camera.autoDegree = value; }
        }

        public float cameraAutoSpeed
        {
            get { return _asset.camera.autoSpeed / 50f; }
            set { _asset.camera.autoSpeed = value; }
        }

        public LayerMask pawnMask
        {
            get { return _asset.pawn.mask; }
            set { _asset.pawn.mask = value; }
        }

        public LayerMask pawnTerrainMask
        {
            get { return _asset.pawn.terrainMask; }
            set { _asset.pawn.terrainMask = value; }
        }

        public float pawnRunSpeed
        {
            get { return _asset.pawn.runSpeed / 10f; }
            set { _asset.pawn.runSpeed = value; }
        }
    }
}