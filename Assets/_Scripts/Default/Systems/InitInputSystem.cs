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
    public class InitInputSystem : IInitializeSystem
    {
        public void Initialize()
        {
            var entities = Contexts.sharedInstance.input.GetEntities();
            foreach (var entity in entities)
            {
                if (entity.hasTransform && entity.hasName)
                {
                    entity.transform.value.gameObject.SetActive((bool)GameConfig.instance[entity.name.value.ToCamelCase()]);
                }
            }
        }
    }
}