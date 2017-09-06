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
    [CustomEditor(typeof(GameConfig))]
    public class GameConfigInspector : Editor
    {
        GameConfig _config;
        static bool _showInputConfig = true;
        static bool _showCameraConfig = true;
        static bool _showFighterConfig = true;

        public override void OnInspectorGUI()
        {
            _config = target as GameConfig;
            var serializedConfig = new SerializedObject(_config);
            var assetProp = serializedConfig.FindProperty("_asset");
            EditorGUILayout.PropertyField(assetProp);
            if (assetProp.objectReferenceValue)
            {
                var asset = (GameConfigAsset)assetProp.objectReferenceValue;
                DictionaryAssetExtension.serializedAsset = new SerializedObject(asset);
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
                    asset.ObjectPropertyField("cameraBlockMask", "Block Mask");
                    asset.FloatPropertyField("cameraMaxDistance", "Max Distance");
                    asset.FloatPropertyField("cameraMinDistance", "Min Distance");
                    asset.FloatPropertyField("cameraHeight", "Height");
                    asset.FloatPropertyField("cameraFlySpeed", "Fly Speed");
                    asset.FloatPropertyField("cameraWalkSpeed", "Walk Speed");
                    asset.FloatPropertyField("cameraLiftSpeed", "Lift Speed");
                    asset.FloatPropertyField("cameraUpDegree", "Up Degree");
                    asset.FloatPropertyField("cameraDownDegree", "Down Degree");
                    bool autolock;
                    if (asset.BoolPropertyField("cameraAutoLock", out autolock, "Auto Lock"))
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
                if (_showInputConfig = EditorGUILayout.Foldout(_showInputConfig, "Fighter"))
                {
                    EditorGUI.indentLevel++;
                    asset.ObjectPropertyField("fighterTerrainMask", "Terrain Mask");
                    asset.FloatPropertyField("fighterWalkSpeed", "Walk Speed");
                    EditorGUI.indentLevel--;
                }
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