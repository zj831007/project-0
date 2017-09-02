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
        public static Processor processor;
        protected virtual void Start()
        {
            processor = GetProcessor();
            processor.Initialize();
        }

        void Update()
        {
            processor.Execute();
            processor.Cleanup();
        }
    }
}