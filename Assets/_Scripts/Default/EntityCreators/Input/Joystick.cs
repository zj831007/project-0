using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.EntityCreators
{
    public sealed class Joystick : TouchArea
    {
        public RectTransform hat;
        public float offset = 25f;

        Vector3 _center;

        void Start()
        {
            _center = hat.transform.position;
        }
        protected override void OnTouchingArea(Vector3 pos)
        {
            Vector3 dir = (pos - _center).normalized;
            hat.localPosition = dir * offset;
            entity.ReplaceDirection(dir);
        }
        protected override void OnTouchAreaEnd()
        {
            hat.localPosition = Vector3.zero;
            if (entity.hasDirection)
            {
                entity.RemoveDirection();
            }
        }
        protected override void Update()
        {
            base.Update();

#if UNITY_EDITOR

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                entity.ReplaceDirection((Vector3.left + Vector3.up).normalized);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                entity.ReplaceDirection((Vector3.left + Vector3.down).normalized);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                entity.ReplaceDirection((Vector3.right + Vector3.down).normalized);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                entity.ReplaceDirection((Vector3.right + Vector3.up).normalized);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                entity.ReplaceDirection(Vector3.right);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                entity.ReplaceDirection(Vector3.down);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                entity.ReplaceDirection(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                entity.ReplaceDirection(Vector3.up);
            }
            else if (Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                if (entity.hasDirection)
                {
                    entity.RemoveDirection();
                }
            }
#endif
        }
    }
}
