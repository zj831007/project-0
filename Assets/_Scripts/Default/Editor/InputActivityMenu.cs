using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Project0;

namespace Project0.Editor
{
    public class InputActivityMenu
    {
        [MenuItem("Assets/Create/InputActivity", false, 12)]
        private static void CreateAsset()
        {
            var dirs = Array.ConvertAll(Selection.assetGUIDs, guid => AssetDatabase.GUIDToAssetPath(guid));
            foreach (var dir in dirs)
            {
                AssetDatabase.CreateAsset(new InputActivity(), GetPath(dir));
            }
        }
        static string GetPath(string dir, int order = 0)
        {
            string path;
            if (order == 0)
            {
                path = Path.Combine(dir, "NewInputActivity.asset");
            }
            else
            {
                path = Path.Combine(dir, $"NewInputActivity{order}.asset");
            }
            var asset = AssetDatabase.LoadAssetAtPath<InputActivity>(path);
            if (asset) return GetPath(dir, order + 1);
            return path;
        }
    }
}