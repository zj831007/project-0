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
    public class GameInitSystem : IInitializeSystem
    {
        public static event Action OnInit = delegate { };

        public GameInitSystem(Contexts contexts)
        {

        }

        public void Initialize()
        {
            foreach (var e in Contexts.sharedInstance.game.GetEntities())
            {
                if (e.hasTransform)
                {
                    e.transform.value.gameObject.SetActive(!e.isInactive);
                }
            }
            OnInit();
        }
    }
}