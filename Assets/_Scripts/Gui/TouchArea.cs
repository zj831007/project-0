using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.Gui
{
    public abstract class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        int? _id;
        bool moved;

        protected abstract void OnTouchAreaEnd();
        protected abstract void OnTouchingArea(Vector3 pos);
        private void Update()
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
