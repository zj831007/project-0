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

namespace Project0.EntityCreators
{
    public class InputEntityCreator : MonoBehaviour
    {
        protected InputEntity entity;

        public void Awake()
        {
            entity = Contexts.sharedInstance.input.CreateEntity();
            entity.AddTransform(transform);
            entity.AddName(name);
        }
    }
}