//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System.IO;
//using Project0;

//namespace Project0.EditorExtensions
//{
//    public class ConfigAnimationClipsMenu
//    {
//        [MenuItem("Assets/Create/Config AnimationClips", true)]
//        private static bool ValidateCreateAsset()
//        {
//            return Menu.GetFiles().Length > 0;
//        }
//        [MenuItem("Assets/Create/Config AnimationClips", false, 20)]
//        private static void CreateAsset()
//        {
//            var files = Menu.GetFiles();
//            foreach (var file in files)
//            {
//                ModelImporter model = AssetImporter.GetAtPath(file) as ModelImporter;
//                if (model)
//                {
//                    }
//                }
//            }
//        }
//    }
//}