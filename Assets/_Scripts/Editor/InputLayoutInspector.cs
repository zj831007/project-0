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
using System.Reflection;
using System.Linq;
using Entitas.Unity.Editor;
using Entitas.Utils;
using Entitas.VisualDebugging.Unity.Editor;
using UnityEditor.SceneManagement;

namespace Project0
{
    [CustomEditor(typeof(InputLayout))]
    public class InputLayoutInspector : Editor
    {
        InputLayout _layout;
        SerializedObject _serializedAsset;

        public override void OnInspectorGUI()
        {
            try {
                _layout = target as InputLayout;
                var serializedLayout = new SerializedObject(_layout);
                var modeProp = serializedLayout.FindProperty("_mode");
                EditorGUILayout.PropertyField(modeProp);
                var creatorsField = _layout.GetType().GetField("_creators", BindingFlags.NonPublic | BindingFlags.Instance);
                EntityDrawer.DrawObjectMember(creatorsField.FieldType, "creators", creatorsField.GetValue(_layout), _layout, creatorsField.SetValue);
                serializedLayout.ApplyModifiedProperties();
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(_layout);
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                }
            }
            catch (Exception e)
            {
                if (Application.isPlaying)
                {
                    Debug.Log("Input Layout is not editable in play mode.");
                }
                else
                {
                    throw e;
                }
            }
        }
    }
}