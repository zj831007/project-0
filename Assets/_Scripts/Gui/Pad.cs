using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.Gui
{
    public  class Pad : TouchArea
    {
        public string EntityName;

        Vector3 _previous;
        bool _moved;
        int step;
        InputEntity _entity;

        private void Start()
        {
            _entity = Contexts.sharedInstance.input.CreateEntity();
            _entity.AddName(EntityName);
        }
        protected override void OnTouchingArea(Vector3 pos)
        {
            if (step < 5)
            {
                if (_previous != pos)
                {
                    step++;
                }
            }
            else
            {
                _entity.ReplacePadDirection(pos - _previous);
            }
            _previous = pos;
        }
        protected override void OnTouchAreaEnd()
        {
            _moved = false;
            step = 0;
            _entity.RemovePadDirection();
        }
    }
}
