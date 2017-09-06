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

namespace Project0.EntityCreators
{
    public class CameraCreator : MonoBehaviour, IGameEntityProvider
    {
        public GameEntity gameEntity
        {
            get { return _entity; }
        }

        GameEntity _entity;

        private void Awake()
        {
            _entity = Contexts.sharedInstance.game.CreateEntity();
            _entity.isCamera = true;
            _entity.AddTransform(transform);
        }
    }
}