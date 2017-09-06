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
    [InitializeOnLoad]
    public class ExitPlayModeOnScriptCompile
    {
        static ExitPlayModeOnScriptCompile _instance = null;

        ExitPlayModeOnScriptCompile()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        ~ExitPlayModeOnScriptCompile()
        {
            EditorApplication.update -= OnEditorUpdate;
            _instance = null;
        }
        static void Unused<T>(T unusedVariable) { }
        static ExitPlayModeOnScriptCompile()
        {
            Unused(_instance);
            _instance = new ExitPlayModeOnScriptCompile();
        }

        static void OnEditorUpdate()
        {
            if (EditorApplication.isPlaying && EditorApplication.isCompiling)
            {
                EditorApplication.isPlaying = false;
            }
        }
    }
}