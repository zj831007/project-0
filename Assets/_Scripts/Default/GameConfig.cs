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
        static GameConfig()
        {
        }

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

        #region cameraFly
        [Range(0f, 100f)]
        [SerializeField]
        float _cameraFly = 50f;
        public float cameraFly
        {
            get { return _cameraFly / 20f; }
        }
        #endregion

        #region cameraWalk
        [Range(0f, 100f)]
        [SerializeField]
        float _cameraWalk = 50f;
        public float cameraWalk
        {
            get { return _cameraWalk / 20f; }
        }
        #endregion

        #region cameraLift
        [Range(0f, 100f)]
        [SerializeField]
        float _cameraLift = 50f;
        public float cameraLift
        {
            get { return _cameraLift / 20f; }
        }
        #endregion

        #region cameraUp
        [Range(0f, 89f)]
        public float cameraUp = 75;
        #endregion

        #region cameraDown
        [Range(0f, -89f)]
        public float cameraDown = -75;
        #endregion

        #region fighterWalk
        [Range(0f, 100f)]
        [SerializeField]
        float _fighterWalk = 50f;
        public float fighterWalk
        {
            get { return _fighterWalk / 20f; }
        }
        #endregion

    }
}