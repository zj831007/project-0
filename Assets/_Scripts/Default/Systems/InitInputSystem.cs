﻿using System;
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
    public class InitInputActivitySystem : IInitializeSystem
    {
        public void Initialize()
        {
            //if (activity.mode == GameConfig.instance.inputMode)
            //{
            //    var entities = Contexts.sharedInstance.input.GetEntities();
            //    foreach (var entity in entities)
            //    {
            //        if (entity.hasTransform && entity.hasName)
            //        {
            //            entity.transform.value.gameObject.SetActive((bool)activity[entity.name.value]);
            //        }
            //    }
            //    return;
            //}
        }
    }
}