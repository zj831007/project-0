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
    public class Menu
    {
        public static string[] GetDirectories()
        {
            var paths = Array.ConvertAll(Selection.assetGUIDs, guid => AssetDatabase.GUIDToAssetPath(guid));
            return Array.FindAll(paths, path => File.GetAttributes(path).HasFlag(FileAttributes.Directory));
        }
        public static string[] GetFiles()
        {
            var paths = Array.ConvertAll(Selection.assetGUIDs, guid => AssetDatabase.GUIDToAssetPath(guid));
            return Array.FindAll(paths, path => File.GetAttributes(path).HasFlag(FileAttributes.Directory) == false);
        }
    }
}