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

namespace Project0.Gui
{
    public class TupleButton : TouchArea
    {
        public string EntityName;
        public bool horizontal;

        InputEntity _entity;
        Vector3 _center;

        protected virtual void Start()
        {
            _center = transform.position;
            _entity = Contexts.sharedInstance.input.CreateEntity();
            _entity.AddName(EntityName);
        }
        protected override void OnTouchingArea(Vector3 pos)
        {
            float dir = 0f;
            if (horizontal)
            {
                if (pos.x > _center.x)
                {
                    dir = 1f;
                }
                else
                {
                    dir = -1f;
                }
            }
            else
            {
                if (pos.y > _center.y)
                {
                    dir = 1f;
                }
                else
                {
                    dir = -1f;
                }
            }
            _entity.ReplaceTupleButtonDirection(dir);
        }
        protected override void OnTouchAreaEnd()
        {
            _entity.RemoveTupleButtonDirection();
        }
    }
}