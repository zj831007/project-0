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
    public class DictionaryAsset : ScriptableObject
    {
        public SerializedObject serializedAsset;
        Type _self;

        public object this[string field]
        {
            get
            {
                if (_self == null) _self = GetType();
                return _self.GetField(field).GetValue(this);
            }
            set
            {
                if (_self == null) _self = GetType();
                _self.GetField(field).SetValue(this, value);
            }
        }
        public bool BoolPropertyField(string name, string displayName = null)
        {
            object objValue;
            return PropertyField(name, displayName, out objValue, () => serializedAsset.FindProperty(name).boolValue);
        }
        public bool BoolPropertyField(string name, out bool value, string displayName = null)
        {
            object objValue;
            var result = PropertyField(name, displayName, out objValue, () => serializedAsset.FindProperty(name).boolValue);
            value = (bool)objValue;
            return result;
        }
        public bool FloatPropertyField(string name, string displayName = null)
        {
            object objValue;
            return PropertyField(name, displayName, out objValue, () => serializedAsset.FindProperty(name).floatValue);
        }
        public bool FloatPropertyField(string name, out float value, string displayName = null)
        {
            object objValue;
            var result = PropertyField(name, displayName, out objValue, () => serializedAsset.FindProperty(name).floatValue);
            value = (float)objValue;
            return result;
        }

        bool PropertyField(string name, string displayName, out object value, Func<object> getValue)
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
                this[name] = getValue();
                value = this[name];
                return true;
            }
            else
            {
                value = this[name];
                return false;
            }
        }
    }
}