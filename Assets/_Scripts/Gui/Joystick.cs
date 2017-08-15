using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.Gui
{
    public abstract class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public RectTransform hat;
        public float offset = 25f;

        Vector3 _center;
        int? _id;

        protected virtual void Start()
        {
            _center = hat.transform.position;
        }
        protected abstract void OnTouchBegin();
        protected abstract void OnTouchEnd();
        protected abstract void OnTouching(Vector3 dir);
        private void Update()
        {
            if (_id != null)
            {
                Vector3 dir = (InputUtils.GetTouchPosition(_id.Value) - _center).normalized;
                OnTouching(dir);
                hat.localPosition = dir * offset;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _id = eventData.pointerId;
            OnTouchBegin();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _id = null;
            hat.localPosition = Vector3.zero;
            OnTouchEnd();
        }
    }
}
