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
    public class GameConfigEditor : Editor
    {
        GameConfig _config;

        public override void OnInspectorGUI()
        {
            _config = target as GameConfig;
            var serializedConfig = new SerializedObject(_config);
            var assetProp = serializedConfig.FindProperty("_asset");
            EditorGUILayout.PropertyField(assetProp);
            if (assetProp.objectReferenceValue)
            {
                var asset = (GameConfigAsset)assetProp.objectReferenceValue;
                var serializedAsset = new SerializedObject(asset);
                asset.serializedAsset = serializedAsset;
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
                asset.FloatPropertyField("cameraDistance");
                asset.FloatPropertyField("cameraHeight");
                asset.FloatPropertyField("cameraFlySpeed");
                asset.FloatPropertyField("cameraWalkSpeed");
                asset.FloatPropertyField("cameraLiftSpeed");
                asset.FloatPropertyField("cameraUpDegree");
                asset.FloatPropertyField("cameraDownDegree");
                bool autolock;
                if (asset.BoolPropertyField("cameraAutoLock", out autolock))
                {
                    if (Application.isPlaying)
                    {
                        var processor = (Processor)Controller.processor["ThirdPersonCameraProcessor"];
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

                asset.FloatPropertyField("fighterWalkSpeed");

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