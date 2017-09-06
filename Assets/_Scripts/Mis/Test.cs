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
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            AppDomain.CurrentDomain.UnhandledException += MyHandler;
        }
        private void OnEnable()
        {
            throw new Exception("asd");

        }
        private void OnDisable()
        {
            //Application.logMessageReceived -= HandleException;
        }
        static void HandleException(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                //Debug.Log(string.Format("{0}", type, condition, stackTrace));
            }
        }
        void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }
    }
}