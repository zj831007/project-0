using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.Gui
{
    public class Joystick : TouchArea
    {
        public string EntityName;
        public RectTransform hat;
        public float offset = 25f;

        InputEntity _entity;
        Vector3 _center;

        protected virtual void Start()
        {
            _center = hat.transform.position;
            _entity = Contexts.sharedInstance.input.CreateEntity();
            _entity.AddName(EntityName);
        }
        protected override void OnTouchingArea(Vector3 pos)
        {
            Vector3 dir = (pos - _center).normalized;
            hat.localPosition = dir * offset;
            _entity.ReplaceJoystickDirection(dir);
        }
        protected override void OnTouchAreaEnd()
        {
            hat.localPosition = Vector3.zero;
            if (_entity.hasJoystickDirection)
            {
                _entity.RemoveJoystickDirection();
            }
        }
        protected override void Update()
        {
            base.Update();

#if UNITY_EDITOR

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                _entity.ReplaceJoystickDirection((Vector3.left + Vector3.up).normalized);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                _entity.ReplaceJoystickDirection((Vector3.left + Vector3.down).normalized);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                _entity.ReplaceJoystickDirection((Vector3.right + Vector3.down).normalized);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                _entity.ReplaceJoystickDirection((Vector3.right + Vector3.up).normalized);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _entity.ReplaceJoystickDirection(Vector3.right);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _entity.ReplaceJoystickDirection(Vector3.down);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _entity.ReplaceJoystickDirection(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                _entity.ReplaceJoystickDirection(Vector3.up);
            }
#endif
        }
    }
}
