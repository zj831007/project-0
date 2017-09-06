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
    public class TerrainCreator : MonoBehaviour, IGameEntityProvider
    {
        public GameEntity gameEntity
        {
            get { return _entity; }
        }

        public Vector3 normal;

        GameEntity _entity;

        private void Awake()
        {
            _entity = Contexts.sharedInstance.game.CreateEntity();
            if (normal != Vector3.zero)
            {
                normal = normal.normalized;//(0.0, 0.9, 0.4)
                _entity.AddNormal(normal);
            }
        }
    }
}