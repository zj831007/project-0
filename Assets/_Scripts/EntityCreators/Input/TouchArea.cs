using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

namespace Project0.EntityCreators
{
    public class TouchArea : InputEntityCreator, IPointerDownHandler, IPointerUpHandler
    {
        bool moved;

        public void OnPointerDown(PointerEventData eventData)
        {
            entity.ReplaceID(eventData.pointerId);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (entity.hasID)
            {
                entity.RemoveID();
            }
            if (entity.hasPosition)
            {
                entity.RemovePosition();
            }
            if (entity.hasPreviousPosition)
            {
                entity.RemovePreviousPosition();
            }
        }
    }
}
