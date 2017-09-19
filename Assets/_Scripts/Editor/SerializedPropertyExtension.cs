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
    public static class SerializedPropertyExtension
    {
        public static SerializedProperty Get(this SerializedProperty prop, int index)
        {
            return prop.GetArrayElementAtIndex(index);
        }
        public static void Add(this SerializedProperty prop, Object elem)
        {
            prop.InsertArrayElementAtIndex(prop.arraySize);
            prop.serializedObject.FindProperty(prop.propertyPath + $".Array.data[{prop.arraySize - 1}]").objectReferenceValue = elem;
        }
        public static void Remove(this SerializedProperty prop, Object elem)
        {
            for (int i = 0; i < prop.arraySize; i++)
            {
                if (prop.GetArrayElementAtIndex(i).objectReferenceValue == elem)
                {
                    prop.DeleteArrayElementAtIndex(i);
                    return;
                }
            }
        }
    }
}