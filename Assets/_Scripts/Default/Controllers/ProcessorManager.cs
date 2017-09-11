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
    public abstract class Controller : MonoBehaviour
    {
        protected abstract Processor GetProcessor();
        protected abstract Processor GetFixedProcessor();
        public static Processor processor;
        public static Processor fixedProcessor;
        private void Start()
        {
            processor = GetProcessor();
            fixedProcessor = GetFixedProcessor();
            processor.Initialize();
            fixedProcessor.Initialize();
        }

        private void Update()
        {
            processor.Execute();
            processor.Cleanup();
        }
        private void FixedUpdate()
        {
            fixedProcessor.Execute();
            fixedProcessor.Cleanup();
        }
    }
}