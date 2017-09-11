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
    public class TerrainCreator : GameEntityCreator
    {
        public Vector3 normal;

        private void Awake()
        {
            entity = Contexts.sharedInstance.game.CreateEntity();
            entity.AddTransform(transform);
            if (normal != Vector3.zero)
            {
                normal = normal.normalized;//(0.0, 0.9, 0.4)
                entity.AddNormal(normal);
            }
        }
    }
}