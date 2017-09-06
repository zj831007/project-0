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
    public class FighterCreator : MonoBehaviour
    {
        public bool isMain;

        private void Awake()
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();
            entity.isFighter = true;
            entity.isMainFighter = isMain;
            entity.AddTransform(transform);
        }
    }
}