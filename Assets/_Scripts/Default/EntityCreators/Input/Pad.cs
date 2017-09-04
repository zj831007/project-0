using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.EntityCreators
{
    public sealed class Pad : TouchArea
    {
        Vector3 _previous;
        bool _moved;
        int step;

        //protected override void OnTouchingArea(Vector3 pos)
        //{
        //    if (step < 5)
        //    {
        //        if (_previous != pos)
        //        {
        //            step++;
        //        }
        //    }
        //    else
        //    {
        //        entity.ReplaceDirection(pos - _previous);
        //    }
        //    _previous = pos;
        //}
        //protected override void OnTouchAreaEnd()
        //{
        //    _moved = false;
        //    step = 0;
        //    if (entity.hasDirection)
        //    {
        //        entity.RemoveDirection();
        //    }
        //}
    }
}
