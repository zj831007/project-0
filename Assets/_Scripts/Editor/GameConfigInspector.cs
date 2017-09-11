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
                if (asset.BoolPropertyField(nameof(_config.isGod), out isGod))
                {
                    if (Application.isPlaying)
                    {
                        if (isGod)
                        {
                            Controller.processor
                                .DeactivateExecuteSystem(nameof(ThirdPersonCameraProcessor))
                                /*.DeactivateExecuteSystem(nameof(ThirdPersonCameraProcessor))*/;
                            Controller.fixedProcessor
                                .DeactivateExecuteSystem(nameof(PlayerRunSystem));
                            Controller.processor
                                .ActivateExecuteSystem(nameof(CameraFlySystem))
                                .ActivateExecuteSystem(nameof(CameraWalkSystem))
                                .ActivateExecuteSystem(nameof(CameraLiftSystem))
                                .ActivateExecuteSystem(nameof(CameraFreeRotationSystem));
                        }
                        else
                        {
                            Controller.processor
                                .DeactivateExecuteSystem(nameof(CameraFlySystem))
                                .DeactivateExecuteSystem(nameof(CameraWalkSystem))
                                .DeactivateExecuteSystem(nameof(CameraLiftSystem))
                                .DeactivateExecuteSystem(nameof(CameraFreeRotationSystem));
                            Controller.processor
                                //.ActivateExecuteSystem(nameof(PlayerRunSystem))
                                .ActivateExecuteSystem(nameof(ThirdPersonCameraProcessor));
                            Controller.fixedProcessor
                                .ActivateExecuteSystem(nameof(PlayerRunSystem));
                        }
                    }
                }
                if (_showInputConfig = EditorGUILayout.Foldout(_showInputConfig, "Input"))
                {
                    EditorGUI.indentLevel++;
                    if (asset.BoolPropertyField(nameof(_config.leftJoystick)))
                    {
                        ActivateInitInputSystem();
                    }
                    if (asset.BoolPropertyField(nameof(_config.rightJoystick)))
                    {
                        ActivateInitInputSystem();
                    }
                    if (asset.BoolPropertyField(nameof(_config.leftTupleButton)))
                    {
                        ActivateInitInputSystem();
                    }
                    bool rightPad;
                    if (asset.BoolPropertyField(nameof(_config.rightPad), out rightPad))
                    {
                        ActivateInitInputSystem();
                    }
                    if (rightPad)
                    {
                        EditorGUI.indentLevel++;
                        asset.FloatPropertyField(nameof(_config.rightPadX));
                        asset.FloatPropertyField(nameof(_config.rightPadY));
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                }
                if (_showCameraConfig = EditorGUILayout.Foldout(_showCameraConfig, "Camera"))
                {
                    EditorGUI.indentLevel++;
                    asset.ObjectPropertyField(nameof(_config.cameraBlockMask), "Block Mask");
                    asset.FloatPropertyField(nameof(_config.cameraMaxDistance), "Max Distance");
                    asset.FloatPropertyField(nameof(_config.cameraMinDistance), "Min Distance");
                    asset.FloatPropertyField(nameof(_config.cameraHeight), "Height");
                    asset.FloatPropertyField(nameof(_config.cameraFlySpeed), "Fly Speed");
                    asset.FloatPropertyField(nameof(_config.cameraWalkSpeed), "Walk Speed");
                    asset.FloatPropertyField(nameof(_config.cameraLiftSpeed), "Lift Speed");
                    asset.FloatPropertyField(nameof(_config.cameraUpDegree), "Up Degree");
                    asset.FloatPropertyField(nameof(_config.cameraDownDegree), "Down Degree");
                    bool autolock;
                    if (asset.BoolPropertyField(nameof(_config.cameraAutoLock), out autolock, "Auto Lock"))
                    {
                        if (Application.isPlaying)
                        {
                            var processor = Controller.processor[nameof(ThirdPersonCameraProcessor)];
                            if (autolock)
                            {
                                processor
                                    .ActivateExecuteSystem(nameof(CameraAutoCheckSystem))
                                    .ActivateExecuteSystem(nameof(CameraAutoLockSystem));
                            }
                            else
                            {
                                processor
                                    .DeactivateExecuteSystem(nameof(CameraAutoCheckSystem))
                                    .DeactivateExecuteSystem(nameof(CameraAutoLockSystem));
                            }
                        }
                    }
                    if (autolock)
                    {
                        EditorGUI.indentLevel++;
                        bool fast;
                        asset.BoolPropertyField(nameof(_config.cameraFastLock), out fast, "Fast");
                        if (fast == false)
                        {
                            EditorGUI.indentLevel++;
                            asset.FloatPropertyField(nameof(_config.cameraAutoTime), "Time");
                            EditorGUI.indentLevel--;
                        }
                        asset.FloatPropertyField(nameof(_config.cameraAutoDegree), "Degree");
                        asset.FloatPropertyField(nameof(_config.cameraAutoSpeed), "Speed");
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                }
                if (_showInputConfig = EditorGUILayout.Foldout(_showInputConfig, "Player"))
                {
                    EditorGUI.indentLevel++;
                    asset.ObjectPropertyField(nameof(_config.playerMask), "Mask");
                    asset.ObjectPropertyField(nameof(_config.playerTerrainMask), "Terrain Mask");
                    asset.FloatPropertyField(nameof(_config.playerRunSpeed), "Run Speed");
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
                            .ExecuteInitializeSystem(nameof(InitInputSystem));
            }
        }

    }
}