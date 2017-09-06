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

namespace Project0.EditorExtensions
{
    public static class DictionaryAssetExtension
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
        public static bool ObjectPropertyField(this DictionaryAsset asset, string name, string displayName = null)
        {
            object objValue;
            return PropertyField(asset, name, displayName, out objValue, () => serializedAsset.FindProperty(name).objectReferenceValue);
        }
        public static bool ObjectPropertyField(this DictionaryAsset asset, string name, out object value, string displayName = null)
        {
            object objValue;
            var result = PropertyField(asset, name, displayName, out objValue, () => serializedAsset.FindProperty(name).objectReferenceValue);
            value = objValue;
            return result;
        }

        static bool PropertyField(this DictionaryAsset asset, string name, string displayName, out object value, Func<object> getValue)
        {
            if (serializedAsset == null) throw new Exception("Didn't set serializedAsset field before draw Propertyfield.");
            EditorGUI.BeginChangeCheck();
            var prop = serializedAsset.FindProperty(name);
            if (prop == null)
            {
                throw new Exception($"The field \"{name}\" is not existed.");
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
}