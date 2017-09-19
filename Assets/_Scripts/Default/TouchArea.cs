using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

namespace Project0
{
    [RequireComponent(typeof(EntityCreator))]
    public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        bool _removePos;
        protected GameEntity _entity;

        private void Start()
        {
            _entity = transform.GetComponent<EntityCreator>().entity;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _entity.ReplaceID(eventData.pointerId);
            _removePos = false;
        }
        protected virtual void FixedUpdate()
        {
            if (_removePos)
            {
                _removePos = false;
                if (_entity.hasID)
                {
                    _entity.RemoveID();
                }
                if (_entity.hasTouchPosition)
                {
                    _entity.RemoveTouchPosition();
                }
                if (_entity.hasPreviousTouchPosition)
                {
                    _entity.RemovePreviousTouchPosition();
                }
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _removePos = true;
        }
    }
}
