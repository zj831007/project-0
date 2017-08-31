using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.EntityCreators
{
    public abstract class TouchArea : InputEntityCreator, IPointerDownHandler, IPointerUpHandler
    {
        int? _id;
        bool moved;

        protected abstract void OnTouchAreaEnd();
        protected abstract void OnTouchingArea(Vector3 pos);
        protected virtual void Update()
        {
            if (_id != null)
            {
                OnTouchingArea(InputUtils.GetTouchPosition(_id.Value));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _id = eventData.pointerId;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _id = null;
            OnTouchAreaEnd();
        }
    }
}
