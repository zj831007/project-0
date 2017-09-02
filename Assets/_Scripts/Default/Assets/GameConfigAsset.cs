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
        public bool IsGod = false;
        public bool LeftJoystick = false;
        public bool RightJoystick = false;
        public bool LeftTupleButton = false;
        public bool RightPad = false;
        [Range(0f, 100f)]
        public float RightPadX = 50f;
        [Range(0f, 100f)]
        public float RightPadY = 50f;
        [Range(0f, 5f)]
        public float CameraDistance = 1f;
        [Range(0f, 5f)]
        public float CameraHeight = 1f;
        [Range(0f, 100f)]
        public float CameraFlySpeed = 50f;
        [Range(0f, 100f)]
        public float CameraWalkSpeed = 50f;
        [Range(0f, 100f)]
        public float CameraLiftSpeed = 50f;
        [Range(0f, 89f)]
        public float CameraUpDegree = 75f;
        [Range(0f, 89f)]
        public float CameraDownDegree = 75f;
        public bool CameraAutoLock = true;
        public bool CameraFastLock = false;
        [Range(0f, 3f)]
        public float CameraAutoTime = 0.5f;
        [Range(-89f, 89f)]
        public float CameraAutoDegree = 15f;
        [Range(0f, 100f)]
        public float CameraAutoSpeed = 50f;
        [Range(0f, 100f)]
        public float FighterWalkSpeed = 50f;
    }
}