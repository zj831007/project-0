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

namespace Project0.Design
{
    public class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public static T instance { get; protected set; }

        protected virtual bool Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this);
                return true;
            }
            Destroy(this);
            return false;
        }
    }
}