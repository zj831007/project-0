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
using UnityEditor;

namespace Project0
{
    public static class DebugMenu
    {
        [MenuItem("GameObject/Debug/Position", false, -99)]
        public static void PrintGlobalPosition()
        {
            if (Selection.activeGameObject != null)
            {
                Debug.Log(Selection.activeGameObject.transform.position);
            }
        }
    }
}