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
using UnityEngine.Serialization;

namespace Project0
{
    public class GameConfigAsset : DictionaryAsset
    {
        [Serializable]
        public class InputConfig
        {
            [ConfigPropertyName("inputMode")]
            public InputMode mode;
            [Range(0f, 100f)]
            public float rightPadX = 50f;
            [Range(0f, 100f)]
            public float rightPadY = 50f;
        }
        [NestedConfigAttribute]
        public InputConfig input;
        [Serializable]
        public class CameraConfig
        {
            [ConfigPropertyName("cameraBlockMask")]
            public LayerMask blockMask;
            [Range(0f, 20f), ConfigPropertyName("cameraMaxDistance")]
            public float maxDistance = 5f;
            [Range(0f, 10f), ConfigPropertyName("cameraMinDistance")]
            public float minDistance = 2f;
            [Range(0f, 5f), ConfigPropertyName("cameraMaxDistance")]
            public float height = 1.5f;
            [Range(0f, 100f), ConfigPropertyName("cameraHeight")]
            public float flySpeed = 50f;
            [Range(0f, 100f), ConfigPropertyName("cameraFlySpeed")]
            public float walkSpeed = 50f;
            [Range(0f, 100f), ConfigPropertyName("cameraWalkSpeed")]
            public float liftSpeed = 50f;
            [Range(0f, 89f), ConfigPropertyName("cameraLiftSpeed")]
            public float upDegree = 75f;
            [Range(-89f, 0f), ConfigPropertyName("cameraUpDegree")]
            public float downDegree = -75f;
            [ConfigPropertyName("cameraAutoLock")]
            public bool autoLock = true;
            [ConfigPropertyName("cameraFastLock")]
            public bool fastLock = false;
            [Range(0f, 3f), ConfigPropertyName("cameraAutoTime")]
            public float autoTime = 0.5f;
            [Range(-89f, 89f), ConfigPropertyName("cameraAutoDegree")]
            public float autoDegree = 15f;
            [Range(0f, 100f), ConfigPropertyName("cameraAutoSpeed")]
            public float autoSpeed = 50f;
        }
        [NestedConfigAttribute]
        public CameraConfig camera;
        [Serializable]
        public class PawnConfig
        {
            [ConfigPropertyName("pawnMask")]
            public LayerMask mask;
            [ConfigPropertyName("pawnTerrainMask")]
            public LayerMask terrainMask;
            [Range(0f, 100f), ConfigPropertyName("pawnRunSpeed")]
            public float runSpeed = 50f;
        }
        [NestedConfigAttribute]
        public PawnConfig pawn;
    }
}