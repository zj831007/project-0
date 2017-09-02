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
                asset
                    .BoolPropertyField("IsGod")
                    .BoolPropertyField("LeftJoystick")
                    .BoolPropertyField("RightJoystick")
                    .BoolPropertyField("LeftTupleButton")
                    .BoolPropertyField("RightPad");
                asset
                    .FloatPropertyField("RightPadX")
                    .FloatPropertyField("RightPadY")
                    .FloatPropertyField("CameraDistance")
                    .FloatPropertyField("CameraHeight")
                    .FloatPropertyField("CameraFlySpeed")
                    .FloatPropertyField("CameraWalkSpeed")
                    .FloatPropertyField("CameraLiftSpeed")
                    .FloatPropertyField("CameraUpDegree")
                    .FloatPropertyField("CameraDownDegree");
                asset
                    .BoolPropertyField("CameraAutoLock")
                    .BoolPropertyField("CameraFastLock");
                asset
                    .FloatPropertyField("CameraAutoTime")
                    .FloatPropertyField("CameraAutoDegree")
                    .FloatPropertyField("CameraAutoSpeed")
                    .FloatPropertyField("FighterWalkSpeed");

            }
            serializedConfig.ApplyModifiedProperties();
        }
    }
}