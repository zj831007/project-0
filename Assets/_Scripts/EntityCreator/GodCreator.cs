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

namespace Project0.EntityCreator
{
    public class GodCreator : MonoBehaviour
    {
        public float speed = 5;

        private void Start()
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();
            entity.isFighter = true;
            entity.AddGod(speed / 100);
            entity.AddTransform(transform);
        }
    }
}