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
        #region inputActivities
        [SerializeField]
        public InputActivity[] _inputActivities;
        public InputActivity[] inputActivities
        {
            get { return _inputActivities; }
        }
        #endregion

        public InputMode inputMode;

        #region rightPadX
        [Range(0f, 100f)]
        [SerializeField]
        float _rightPadX = 50f;
        public float rightPadX
        {
            get { return _rightPadX / 10f; }
        }
        #endregion

        #region rightPadY
        [Range(0f, 100f)]
        [SerializeField]
        float _rightPadY = 50f;
        public float rightPadY
        {
            get { return _rightPadY / 10f; }
        }
        #endregion

        #region cameraDistance
        [Range(0f, 5f)]
        public float cameraDistance = 1f;
        #endregion

        #region cameraHeight
        [Range(0f, 5f)]
        [SerializeField]
        float _cameraHeight = 1f;
        public Vector3 cameraHeight
        {
            get { return new Vector3(0f, _cameraHeight, 0f); }
        }
        #endregion

        #region cameraFlySpeed
        [Range(0f, 100f)]
        [SerializeField]
        float _cameraFlySpeed = 50f;
        public float cameraFlySpeed
        {
            get { return _cameraFlySpeed / 20f; }
        }
        #endregion

        #region cameraWalkSpeed
        [Range(0f, 100f)]
        [SerializeField]
        float _cameraWalkSpeed = 50f;
        public float cameraWalkSpeed
        {
            get { return _cameraWalkSpeed / 20f; }
        }
        #endregion

        #region cameraLiftSpeed
        [Range(0f, 100f)]
        [SerializeField]
        float _cameraLiftSpeed = 50f;
        public float cameraLiftSpeed
        {
            get { return _cameraLiftSpeed / 20f; }
        }
        #endregion

        #region cameraUpDegree
        [Range(0f, 89f)]
        public float cameraUpDegree = 75;
        #endregion

        #region cameraDownDegree
        [Range(0f, -89f)]
        public float cameraDownDegree = -75;
        #endregion

        public bool cameraAutoLock = true;
        public bool cameraFastLock = false;
        public float cameraAutoTime = 0.5f;

        #region cameraAutoDegree
        [Range(-89f, 89f)]
        public float cameraAutoDegree = 15f;
        #endregion

        #region cameraAutoSpeed
        [Range(0f, 100f)]
        [SerializeField]
        public float _cameraAutoSpeed = 50f;
        public float cameraAutoSpeed
        {
            get { return _cameraAutoSpeed / 50f; }
        }
        #endregion

        #region fighterWalkSpeed
        [Range(0f, 100f)]
        [SerializeField]
        float _fighterWalkSpeed = 50f;
        public float fighterWalkSpeed
        {
            get { return _fighterWalkSpeed / 20f; }
        }
        #endregion

    }
}