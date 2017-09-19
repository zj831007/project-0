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
using System.Linq;
using System.Reflection;
using Entitas.Unity.Editor;
using Entitas.Utils;
using Entitas.VisualDebugging.Unity.Editor;
using UnityEditor.SceneManagement;

namespace Project0
{
    [CustomEditor(typeof(EntityCreator))]
    public class EntityCreatorInspector : Editor
    {
        List<int> indices;
        List<SerializableComponent> components;
        string componentNameSearchString = "";
        SerializedObject _serializedCreator;
        Transform _transform;
        Dictionary<int, GUIStyle> styles = new Dictionary<int, GUIStyle>();
        bool expendNewComponent;

        public override void OnInspectorGUI()
        {
            var creator = target as EntityCreator;
            _serializedCreator = new SerializedObject(creator);
            _transform = creator.transform;
            var componentsField = creator.GetType().GetField("_components", BindingFlags.NonPublic | BindingFlags.Instance);
            var componentsProp = _serializedCreator.FindProperty("_components");
            components = (componentsField.GetValue(creator) as SerializableComponent[])?.ToList() ?? new List<SerializableComponent>();

            components = components.FindAll(c => c != null);
            indices = components.ConvertAll(c => Array.FindIndex(GameComponentsLookup.componentTypes, t => t == c.GetType()));

            if (expendNewComponent)
            {
                componentsProp.GetArrayElementAtIndex(components.Count - 1).isExpanded = true;
                expendNewComponent = false;
            }

            EntitasEditorLayout.BeginVerticalBox();
            {
                EditorGUILayout.LabelField($"Components ({components.Count})", EditorStyles.boldLabel);
                EditorGUILayout.Space();
                AddComponentMenu();
                EditorGUILayout.Space();
                componentNameSearchString = EntitasEditorLayout.SearchTextField(componentNameSearchString);
                EditorGUILayout.Space();
                for (int i = 0; i < components.Count && i < componentsProp.arraySize; i++)
                {
                    DrawComponent(indices[i], componentsProp.Get(i));
                }
            }
            EntitasEditorLayout.EndVerticalBox();
            componentsField.SetValue(creator, components.ToArray());
            EditorUtility.SetDirty(creator);
            _serializedCreator.ApplyModifiedProperties();
            if (GUI.changed)
            {
                if (Application.isPlaying)
                {
                    Debug.Log("Entity Creator is not editable in play mode.");
                }
                else
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                }
            }
        }
        void AddComponentMenu()
        {
            var entitasType = typeof(SerializableComponent);
            var iComponentType = typeof(IComponent);
            var componentTypes = GameComponentsLookup.componentTypes
                .Where(type => entitasType.IsAssignableFrom(type) && iComponentType.IsAssignableFrom(type))
                .ToArray();
            var componentNames = Array.ConvertAll(componentTypes, com => com.Name.RemoveComponentSuffix());
            if (GameComponentsLookup.componentNames.Length != styles.Count)
            {
                for (int i = 0; i < GameComponentsLookup.componentTypes.Length; i++)
                {
                    var hue = (float)i / GameComponentsLookup.componentTypes.Length;
                    var componentColor = Color.HSVToRGB(hue, 0.7f, 1f);
                    componentColor.a = 0.15f;
                    var style = new GUIStyle(GUI.skin.box);
                    style.padding = new RectOffset(13, 3, 3, 3);
                    style.normal.background = createTexture(2, 2, componentColor);
                    styles[i] = style;
                }
            }
            var selected = EditorGUILayout.Popup("Add Component", -1, componentNames);
            if (selected >= 0)
            {
                var type = componentTypes[selected];
                var name = componentNames[selected];
                var index = Array.FindIndex(GameComponentsLookup.componentNames, n => n == name);
                if (indices.Contains(index) == false)
                {
                    indices.Add(index);
                    var newCom = (SerializableComponent)CreateInstance(type);
                    var infos = type.GetPublicMemberInfos();
                    foreach (var info in infos)
                    {
                        var dva = Array.Find(info.attributes, a => a.attribute.GetType() == typeof(DefaultValuePathAttribute))?.attribute as DefaultValuePathAttribute;
                        if (dva != null)
                        {
                            object value;
                            if (EntityDrawer.CreateDefualtValueByPath(dva.path, _transform, out value))
                            {
                                info.SetValue(newCom, value);
                            }
                        }
                    }
                    components.Add(newCom);
                    expendNewComponent = true;
                }
            }
        }
        static Texture2D createTexture(int width, int height, Color color)
        {
            var pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; ++i)
            {
                pixels[i] = color;
            }
            var result = new Texture2D(width, height);
            result.SetPixels(pixels);
            result.Apply();
            return result;
        }
        void DrawComponent(int index, SerializedProperty prop)
        {
            var component = (SerializableComponent)prop.objectReferenceValue;
            var componentType = component.GetType();
            var componentName = componentType.Name.RemoveComponentSuffix();
            if (EntitasEditorLayout.MatchesSearchString(componentName.ToLower(), componentNameSearchString.ToLower()))
            {
                var boxStyle = styles[index];
                EditorGUILayout.BeginVertical(boxStyle);
                {
                    var memberInfos = componentType.GetPublicMemberInfos();
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (memberInfos.Count == 0)
                        {
                            EditorGUILayout.LabelField(componentName, EditorStyles.boldLabel);
                        }
                        else
                        {
                            var foldoutStyle = new GUIStyle(EditorStyles.foldout);
                            foldoutStyle.fontStyle = FontStyle.Bold;
                            prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, componentName, foldoutStyle);
                        }
                        if (EntitasEditorLayout.MiniButton("-"))
                        {
                            components.Remove(component);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    if (prop.isExpanded)
                    {
                        foreach (var info in memberInfos)
                        {
                            var memberValue = info.GetValue(component);
                            if (memberValue != null && memberValue.Equals(null))
                            {
                                info.SetValue(component, null);
                                memberValue = null;
                            }
                            var memberType = memberValue == null ? info.type : memberValue.GetType();
                            EntityDrawer.DrawObjectMember(info, memberValue, component, info.SetValue, _transform);
                        }
                    }
                }
                EntitasEditorLayout.EndVerticalBox();
            }
        }
        static IComponentDrawer GetComponentDrawer(Type type)
        {
            foreach (var drawer in EntityDrawer._componentDrawers)
            {
                if (drawer.HandlesType(type))
                {
                    return drawer;
                }
            }

            return null;
        }
    }
}