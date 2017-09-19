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
namespace Project0
{
    public class EntityCreator : MonoBehaviour
    {
        public GameEntity entity { get; private set; }

        [SerializeField]
        SerializableComponent[] _components;

        private void Awake()
        {
            entity = Contexts.sharedInstance.game.CreateEntity();
            for (int i = 0; i < _components.Length; i++)
            {
                var com = _components[i];
                var index = Array.FindIndex(GameComponentsLookup.componentTypes, t => t == com.GetType());
                entity.AddComponent(index, (IComponent)com);
            }
            entity.AddTransform(transform);
        }
    }
}