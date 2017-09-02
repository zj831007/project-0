using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Project0;

namespace Project0.EditorExtensions
{
    public class GameConfigMenu
    {
        [MenuItem("Assets/Create/GameConfigAsset", false, 12)]
        private static void CreateAsset()
        {
            var dirs = Array.ConvertAll(Selection.assetGUIDs, guid => AssetDatabase.GUIDToAssetPath(guid));
            foreach (var dir in dirs)
            {
                AssetDatabase.CreateAsset(new GameConfigAsset(), GetPath(dir));
            }
        }
        static string GetPath(string dir, int order = 0)
        {
            string path;
            if (order == 0)
            {
                path = Path.Combine(dir, "NewGameConfig.asset");
            }
            else
            {
                path = Path.Combine(dir, $"NewGameConfig{order}.asset");
            }
            var asset = AssetDatabase.LoadAssetAtPath<GameConfigAsset>(path);
            if (asset) return GetPath(dir, order + 1);
            return path;
        }
    }
}