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
    public abstract class ProcessorManager : SceneSingleton<ProcessorManager>
    {
        public abstract InputMode inputMode { get; set; }
        public abstract bool autoLock { get; set; }
        public float timeInterpolation { get; private set; }
        public bool physicsDirty { get; private set; }

        protected abstract Processor viewProcessor { get; }
        protected abstract Processor processor { get; }
        float _nextFixedTime;
        float _fixedTime;

        protected override bool Awake()
        {
            base.Awake();
            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                go.SetActive(true);
            }
            return true;
        }
        private void Update()
        {
            var _lastTime = Mathf.Max(_fixedTime, Time.time - Time.deltaTime);
            timeInterpolation = (Time.time - _lastTime) / (_nextFixedTime - _lastTime);
            viewProcessor.Execute();
            viewProcessor.Cleanup();
            physicsDirty = false;
        }
        private void FixedUpdate()
        {
            _fixedTime = Time.time;
            _nextFixedTime = Time.time + Time.fixedDeltaTime;
            processor.Execute();
            processor.Cleanup();
            physicsDirty = true;
        }
    }
}