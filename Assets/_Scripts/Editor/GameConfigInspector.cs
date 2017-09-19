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
using UnityEditor.SceneManagement;

namespace Project0.EditorExtensions
{
    [CustomEditor(typeof(GameConfig))]
    public class GameConfigInspector : Editor
    {
        GameConfig _config;
        GameConfigAsset _asset;
        SerializedObject _serializedAsset;

        public override void OnInspectorGUI()
        {
            _config = target as GameConfig;
            var serializedConfig = new SerializedObject(_config);
            var assetProp = serializedConfig.FindProperty("_asset");
            EditorGUILayout.PropertyField(assetProp);
            if (assetProp.objectReferenceValue)
            {
                _asset = (GameConfigAsset)assetProp.objectReferenceValue;
                _serializedAsset = new SerializedObject(_asset);
                DrawProperties(_asset.GetType());
                EditorUtility.SetDirty(_asset);
            }
            serializedConfig.ApplyModifiedProperties();
            if (GUI.changed)
            {
                EditorUtility.SetDirty(_config);
                if (Application.isPlaying == false)
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                }
            }
        }
        public void DrawProperties(Type type, string parentPath = "")
        {
            foreach (var field in type.GetFields())
            {
                if (field.GetCustomAttribute<NestedConfigAttribute>() == null)
                {
                    EditorGUI.BeginChangeCheck();
                    var prop = _serializedAsset.FindProperty(parentPath + field.Name);
                    EditorGUILayout.PropertyField(prop);
                    if (EditorGUI.EndChangeCheck() == false) continue;
                    var name = field.GetCustomAttribute<ConfigPropertyNameAttribute>()?.name ?? field.Name;
                    switch (prop.propertyType)
                    {
                        case SerializedPropertyType.Generic:
                            break;
                        case SerializedPropertyType.Integer:
                            break;
                        case SerializedPropertyType.Boolean:
                            _config[name] = prop.boolValue;
                            break;
                        case SerializedPropertyType.Float:
                            _config[name] = prop.floatValue;
                            break;
                        case SerializedPropertyType.String:
                            break;
                        case SerializedPropertyType.Color:
                            break;
                        case SerializedPropertyType.ObjectReference:
                            break;
                        case SerializedPropertyType.LayerMask:
                            break;
                        case SerializedPropertyType.Enum:
                            _config[name] = prop.enumValueIndex;
                            break;
                        case SerializedPropertyType.Vector2:
                            break;
                        case SerializedPropertyType.Vector3:
                            break;
                        case SerializedPropertyType.Vector4:
                            break;
                        case SerializedPropertyType.Rect:
                            break;
                        case SerializedPropertyType.ArraySize:
                            break;
                        case SerializedPropertyType.Character:
                            break;
                        case SerializedPropertyType.AnimationCurve:
                            break;
                        case SerializedPropertyType.Bounds:
                            break;
                        case SerializedPropertyType.Gradient:
                            break;
                        case SerializedPropertyType.Quaternion:
                            break;
                        case SerializedPropertyType.ExposedReference:
                            break;
                        case SerializedPropertyType.FixedBufferSize:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    var prop = _serializedAsset.FindProperty(field.Name);
                    if (prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, field.Name))
                    {
                        EditorGUI.indentLevel++;
                        DrawProperties(field.GetValue(_asset).GetType(), prop.propertyPath + ".");
                        EditorGUI.indentLevel--;
                    }
                }
            }
        }
    }
}