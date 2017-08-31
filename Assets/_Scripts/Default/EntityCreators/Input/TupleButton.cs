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
    public class TupleButton : TouchArea
    {
        public bool horizontal;

        Vector3 _center;

        protected virtual void Start()
        {
            _center = transform.position;
        }
        protected override void OnTouchingArea(Vector3 pos)
        {
            Vector3 dir;
            if (horizontal)
            {
                if (pos.x > _center.x)
                {
                    dir = new Vector3(1f, 0f, 0f);
                }
                else
                {
                    dir = new Vector3(-1f, 0f, 0f);
                }
            }
            else
            {
                if (pos.y > _center.y)
                {
                    dir = new Vector3(0f, 1f, 0f);
                }
                else
                {
                    dir = new Vector3(0f, -1f, 0f);
                }
            }
            entity.ReplaceDirection(dir);
        }
        protected override void OnTouchAreaEnd()
        {
            if (entity.hasDirection)
            {
                entity.RemoveDirection();
            }
        }
    }
}