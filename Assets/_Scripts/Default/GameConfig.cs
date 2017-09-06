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
        Type _self;

        public object this[string field]
        {
            get
            {
                if (_self == null) _self = GetType();
                return _self.GetProperty(field).GetValue(this);
            }
            set
            {
                if (_self == null) _self = GetType();
                _self.GetProperty(field).SetValue(this, value);
            }
        }

        [SerializeField]
        GameConfigAsset _asset;

        public bool isGod
        {
            get { return _asset.isGod; }
        }

        public bool leftJoystick
        {
            get { return _asset.leftJoystick; }
        }

        public bool rightJoystick
        {
            get { return _asset.rightJoystick; }
        }

        public bool leftTupleButton
        {
            get { return _asset.leftTupleButton; }
        }

        public bool rightPad
        {
            get { return _asset.rightPad; }
        }

        public float rightPadX
        {
            get { return _asset.rightPadX / 20f; }
        }

        public float rightPadY
        {
            get { return _asset.rightPadY / 20f; }
        }

        public LayerMask cameraBlockMask
        {
            get { return _asset.cameraBlockMask; }
        }

        public float cameraMaxDistance
        {
            get { return _asset.cameraMaxDistance; }
        }

        public float cameraMinDistance
        {
            get { return _asset.cameraMinDistance; }
        }

        public Vector3 cameraHeight
        {
            get { return new Vector3(0f, _asset.cameraHeight, 0f); }
        }

        public float cameraFlySpeed
        {
            get { return _asset.cameraFlySpeed / 20f; }
        }

        public float cameraWalkSpeed
        {
            get { return _asset.cameraWalkSpeed / 20f; }
        }

        public float cameraLiftSpeed
        {
            get { return _asset.cameraLiftSpeed / 20f; }
        }

        public float cameraUpDegree
        {
            get { return _asset.cameraUpDegree; }
        }

        public float cameraDownDegree
        {
            get { return _asset.cameraDownDegree; }
        }

        public bool cameraAutoLock
        {
            get { return _asset.cameraAutoLock; }
        }
        public bool cameraFastLock
        {
            get { return _asset.cameraFastLock; }
        }

        public float cameraAutoTime
        {
            get { return _asset.cameraAutoTime; }
        }

        public float cameraAutoDegree
        {
            get { return _asset.cameraAutoDegree; }
        }

        public float cameraAutoSpeed
        {
            get { return _asset.cameraAutoSpeed / 50f; }
        }

        public LayerMask fighterTerrainMask
        {
            get { return _asset.fighterTerrainMask; }
        }

        public float fighterWalkSpeed
        {
            get { return _asset.fighterWalkSpeed / 20f; }
        }
    }
}