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

namespace Project0.EditorExtensions
{
    public static class DictionaryAssetExtensions
    {
        public static SerializedObject serializedAsset;

        public static bool BoolPropertyField(this DictionaryAsset asset, string name, string displayName = null)
        {
            object objValue;
            return PropertyField(asset, name, displayName, out objValue, () => serializedAsset.FindProperty(name).boolValue);
        }
        public static bool BoolPropertyField(this DictionaryAsset asset, string name, out bool value, string displayName = null)
        {
            object objValue;
            var result = PropertyField(asset, name, displayName, out objValue, () => serializedAsset.FindProperty(name).boolValue);
            value = (bool)objValue;
            return result;
        }
        public static bool FloatPropertyField(this DictionaryAsset asset, string name, string displayName = null)
        {
            object objValue;
            return PropertyField(asset, name, displayName, out objValue, () => serializedAsset.FindProperty(name).floatValue);
        }
        public static bool FloatPropertyField(this DictionaryAsset asset, string name, out float value, string displayName = null)
        {
            object objValue;
            var result = PropertyField(asset, name, displayName, out objValue, () => serializedAsset.FindProperty(name).floatValue);
            value = (float)objValue;
            return result;
        }

        static bool PropertyField(this DictionaryAsset asset, string name, string displayName, out object value, Func<object> getValue)
        {
            if (serializedAsset == null) throw new Exception("Didn't set serializedAsset field before draw Propertyfield.");
            EditorGUI.BeginChangeCheck();
            var prop = serializedAsset.FindProperty(name);
            if (prop == null)
            {
                throw new Exception($"The field \"${name}\" is not existed.");
            }
            EditorGUILayout.PropertyField(prop, displayName != null ? new GUIContent(displayName) : null);
            if (EditorGUI.EndChangeCheck())
            {
                asset[name] = getValue();
                value = asset[name];
                return true;
            }
            else
            {
                value = asset[name];
                return false;
            }
        }
    }

    [CustomEditor(typeof(GameConfig))]
    public class GameConfigInspector : Editor
    {
        GameConfig _config;
        bool _showInputConfig;
        bool _showCameraConfig;

        public override void OnInspectorGUI()
        {
            _config = target as GameConfig;
            var serializedConfig = new SerializedObject(_config);
            var assetProp = serializedConfig.FindProperty("_asset");
            EditorGUILayout.PropertyField(assetProp);
            if (assetProp.objectReferenceValue)
            {
                var asset = (GameConfigAsset)assetProp.objectReferenceValue;
                DictionaryAssetExtensions.serializedAsset = new SerializedObject(asset);
                bool isGod;
                if (asset.BoolPropertyField("isGod", out isGod))
                {
                    if (Application.isPlaying)
                    {
                        if (isGod)
                        {
                            Controller.processor
                                .DeactivateExecuteSystem("MainFighterWalkSystem")
                                .DeactivateExecuteSystem("ThirdPersonCameraProcessor");
                            Controller.processor
                                .ActivateExecuteSystem("CameraFlySystem")
                                .ActivateExecuteSystem("CameraWalkSystem")
                                .ActivateExecuteSystem("CameraLiftSystem")
                                .ActivateExecuteSystem("CameraFreeRotationSystem");
                        }
                        else
                        {
                            Controller.processor
                                .DeactivateExecuteSystem("CameraFlySystem")
                                .DeactivateExecuteSystem("CameraWalkSystem")
                                .DeactivateExecuteSystem("CameraLiftSystem")
                                .DeactivateExecuteSystem("CameraFreeRotationSystem");
                            Controller.processor
                                .ActivateExecuteSystem("MainFighterWalkSystem")
                                .ActivateExecuteSystem("ThirdPersonCameraProcessor");
                        }
                    }
                }
                if (_showInputConfig = EditorGUILayout.Foldout(_showInputConfig, "Input"))
                {
                    EditorGUI.indentLevel++;
                    if (asset.BoolPropertyField("leftJoystick"))
                    {
                        ActivateInitInputSystem();
                    }
                    if (asset.BoolPropertyField("rightJoystick"))
                    {
                        ActivateInitInputSystem();
                    }
                    if (asset.BoolPropertyField("leftTupleButton"))
                    {
                        ActivateInitInputSystem();
                    }
                    bool rightPad;
                    if (asset.BoolPropertyField("rightPad", out rightPad))
                    {
                        ActivateInitInputSystem();
                    }
                    if (rightPad)
                    {
                        EditorGUI.indentLevel++;
                        asset.FloatPropertyField("rightPadX");
                        asset.FloatPropertyField("rightPadY");
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                }
                if (_showCameraConfig = EditorGUILayout.Foldout(_showCameraConfig, "Camera"))
                {
                    EditorGUI.indentLevel++;
                    asset.FloatPropertyField("cameraDistance", "Distance");
                    asset.FloatPropertyField("cameraHeight", "Height");
                    asset.FloatPropertyField("cameraFlySpeed", "FlySpeed");
                    asset.FloatPropertyField("cameraWalkSpeed", "WalkSpeed");
                    asset.FloatPropertyField("cameraLiftSpeed", "LiftSpeed");
                    asset.FloatPropertyField("cameraUpDegree", "UpDegree");
                    asset.FloatPropertyField("cameraDownDegree", "DownDegree");
                    bool autolock;
                    if (asset.BoolPropertyField("cameraAutoLock", out autolock, "AutoLock"))
                    {
                        if (Application.isPlaying)
                        {
                            var processor = Controller.processor["ThirdPersonCameraProcessor"];
                            if (autolock)
                            {
                                processor
                                    .ActivateExecuteSystem("CameraAutoCheckSystem")
                                    .ActivateExecuteSystem("CameraAutoLockSystem");
                            }
                            else
                            {
                                processor
                                    .DeactivateExecuteSystem("CameraAutoCheckSystem")
                                    .DeactivateExecuteSystem("CameraAutoLockSystem");
                            }
                        }
                    }
                    if (autolock)
                    {
                        EditorGUI.indentLevel++;
                        bool fast;
                        asset.BoolPropertyField("cameraFastLock", out fast, "Fast");
                        if (fast == false)
                        {
                            EditorGUI.indentLevel++;
                            asset.FloatPropertyField("cameraAutoTime", "Time");
                            EditorGUI.indentLevel--;
                        }
                        asset.FloatPropertyField("cameraAutoDegree", "Degree");
                        asset.FloatPropertyField("cameraAutoSpeed", "Speed");
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                }
                asset.FloatPropertyField("fighterWalkSpeed");
                EditorUtility.SetDirty(asset);
            }
            serializedConfig.ApplyModifiedProperties();
        }
        public void ActivateInitInputSystem()
        {
            if (Application.isPlaying)
            {
                Controller.processor
                            .ExecuteInitializeSystem("InitInputSystem");
            }
        }

    }
}