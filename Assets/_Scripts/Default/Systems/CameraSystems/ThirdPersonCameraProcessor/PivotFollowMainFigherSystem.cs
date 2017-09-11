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
    public class PivotFollowPlayerSystem : IExecuteSystem
    {
        GameContext _game;
        Vector3 _vel;
        GameEntity _pivot;

        public PivotFollowPlayerSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var camera = ThirdPersonCameraProcessor.camera;
            var player = _game.playerEntity;
            if (player != null && player.hasTransform)
            {
                var pivotTransform = camera.pivotTransform.value;
                pivotTransform.position = Vector3.SmoothDamp(pivotTransform.position, player.transform.value.position + GameConfig.instance.cameraHeight, ref _vel, 0.2f);
            }
        }

        //public void Initialize()
        //{
        //    //var camera = _game.cameraEntity;
        //    //var fighter = _game.mainFighterEntity;
        //    //if (camera != null && fighter != null && fighter.hasTransform)
        //    //{
        //    //    _pivot = Contexts.sharedInstance.game.CreateEntity();
        //    //    var pivotTransform = new GameObject("CameraPivot").transform;
        //    //    pivotTransform.position = fighter.transform.value.position + GameConfig.instance.cameraHeight;
        //    //    _pivot.AddTransform(pivotTransform);
        //    //    camera.AddPivot(_pivot);
        //    //}
        //}

        //public void TearDown()
        //{
        //    //if (_pivot != null)
        //    //{
        //    //    var camera = _game.cameraEntity;
        //    //    if (camera != null && camera.hasPivot)
        //    //    {
        //    //        camera.RemovePivot();
        //    //    }
        //    //    _pivot.isDestroyed = true;
        //    //    _pivot = null;
        //    //}
        //}
    }
}