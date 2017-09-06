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
    public class FighterCreator : MonoBehaviour, IGameEntityProvider
    {
        public bool isMain;

        public GameEntity gameEntity
        {
            get { return _entity; }
        }
        GameEntity _entity;

        private void Awake()
        {
            _entity = Contexts.sharedInstance.game.CreateEntity();
            _entity.isFighter = true;
            _entity.isMainFighter = isMain;
            _entity.AddTransform(transform);
        }
    }
}