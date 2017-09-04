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
    public class FindMissingScriptsRecursively : EditorWindow
    {
        static int go_count = 0, components_count = 0, missing_count = 0;

        [MenuItem("Window/FindMissingScriptsRecursively")]
        public static void ShowWindow()
        {
            GetWindow(typeof(FindMissingScriptsRecursively));
        }

        public void OnGUI()
        {
            if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
            {
                CleanupMissingScripts();
            }
        }
        static void CleanupMissingScripts()
        {
            var gameObjects = FindObjectsOfType<GameObject>();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                var gameObject = gameObjects[i];
                var components = gameObject.GetComponents<Component>();
                var serializedObject = new SerializedObject(gameObject);
                var prop = serializedObject.FindProperty("m_Component");
                int r = 0;
                for (int j = 0; j < components.Length; j++)
                {
                    if (components[j] == null)
                    {
                        DestroyImmediate(components[j - r]);
                        r++;
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
        private static void FindInSelected()
        {
            GameObject[] gos = Selection.gameObjects;
            go_count = 0;
            components_count = 0;
            missing_count = 0;
            foreach (GameObject g in gos)
            {
                FindInGO(g);
            }
            Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
        }


        private static void FindInGO(GameObject g)
        {
            go_count++;
            Component[] components = g.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                components_count++;
                if (components[i] == null)
                {
                    missing_count++;
                    string s = g.name;
                    Transform t = g.transform;
                    while (t.parent != null)
                    {
                        s = t.parent.name + "/" + s;
                        t = t.parent;
                    }
                    DestroyImmediate(components[i]);
                    Debug.Log(s + " has an empty script attached in position: " + i, g);
                }
            }
            foreach (Transform childT in g.transform)
            {
                FindInGO(childT.gameObject);
            }
        }
    }

}