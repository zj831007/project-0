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
    public class GameConfigAsset : DictionaryAsset
    {
        public bool isGod = false;
        public bool leftJoystick = false;
        public bool rightJoystick = false;
        public bool leftTupleButton = false;
        public bool rightPad = false;
        [Range(0f, 100f)]
        public float rightPadX = 50f;
        [Range(0f, 100f)]
        public float rightPadY = 50f;
        public LayerMask cameraBlockMask;
        [Range(0f, 20f)]
        public float cameraMaxDistance = 5f;
        [Range(0f, 10f)]
        public float cameraMinDistance = 2f;
        [Range(0f, 5f)]
        public float cameraHeight = 1.5f;
        [Range(0f, 100f)]
        public float cameraFlySpeed = 50f;
        [Range(0f, 100f)]
        public float cameraWalkSpeed = 50f;
        [Range(0f, 100f)]
        public float cameraLiftSpeed = 50f;
        [Range(0f, 89f)]
        public float cameraUpDegree = 75f;
        [Range(-89f, 0f)]
        public float cameraDownDegree = -75f;
        public bool cameraAutoLock = true;
        public bool cameraFastLock = false;
        [Range(0f, 3f)]
        public float cameraAutoTime = 0.5f;
        [Range(-89f, 89f)]
        public float cameraAutoDegree = 15f;
        [Range(0f, 100f)]
        public float cameraAutoSpeed = 50f;
        public LayerMask playerMask;
        public LayerMask playerTerrainMask;
        [Range(0f, 100f)]
        public float playerRunSpeed = 50f;
    }
}