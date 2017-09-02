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
        public DictionaryAsset BoolPropertyField(string name) =>
            PropertyField(name, () => serializedAsset.FindProperty(name).boolValue);
        public DictionaryAsset FloatPropertyField(string name) =>
            PropertyField(name, () => serializedAsset.FindProperty(name).floatValue);

        DictionaryAsset PropertyField(string name, Func<object> getValue)
        {
            if (serializedAsset == null) throw new Exception("Didn't set serializedAsset field before draw Propertyfield");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedAsset.FindProperty(name));
            if (EditorGUI.EndChangeCheck())
            {
                this[name] = getValue();
            }
            return this;
        }
    }
}