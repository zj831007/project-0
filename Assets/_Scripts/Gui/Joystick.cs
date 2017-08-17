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
            _entity.RemoveJoystickDirection();
        }
    }
}
