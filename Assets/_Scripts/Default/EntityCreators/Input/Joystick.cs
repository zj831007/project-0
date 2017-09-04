using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0.EntityCreators
{
    public sealed class Joystick : TouchArea
    {
        public RectTransform hat;
        public float distance = 25f;

        Vector3 _center;
        InputEntity _hatEntity;

        protected override void Awake()
        {
            base.Awake();
            var hatEntity = Contexts.sharedInstance.input.CreateEntity();
            hatEntity.AddTransform(hat);
            hatEntity.AddCenter(hat.transform.position);
            hatEntity.AddDistance(distance);
            entity.AddHat(hatEntity);
        }
#if UNITY_EDITOR
        public void Update()
        {
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
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                if (entity.hasDirection)
                {
                    entity.RemoveDirection();
                }
            }
        }
#endif
    }
}
