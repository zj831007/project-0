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
    public static class GameEntityExtension
    {
        public static GameEntity GetCameraEntity(this GameEntity entity)
        {
            if (entity.hasCameraCreator && entity.cameraCreator.value.entity.isCamera)
            {
                return entity.cameraCreator.value.entity;
            }
            return null;
        }
        public static GameEntity GetTargetEntity(this GameEntity entity)
        {
            if (entity.targetCreator)
            {
                return entity.targetCreator.value.entity;
            }
            return null;
        }
        public static GameEntity GetGamePivotEntity(this GameEntity entity)
        {
            if (entity.pivotCreator && entity.pivotCreator.value.entity.isPivot)
            {
                return entity.pivotCreator.value.entity;
            }
            return null;
        }
        public static GameEntity GetPawnEntity(this GameEntity entity)
        {
            if (entity.hasPawnCreator && entity.pawnCreator.value.entity.isPawn)//pass
            {
                return entity.pawnCreator.value.entity;
            }
            return null;
        }
        public static GameEntity GetHatEntity(this GameEntity entity)
        {
            if (entity.hasHatCreator && entity.hatCreator.value.entity.isHat)//pass
            {
                return entity.hatCreator.value.entity;
            }
            return null;
        }
        public static GameEntity GetPivotEntity(this GameEntity entity)
        {
            if (entity.hasPivotCreator && entity.pivotCreator.value.entity.isPivot)//pass
            {
                return entity.pivotCreator.value.entity;
            }
            return null;
        }
    }
}