using UnityEngine;
using Project0;
using UnityEngine.EventSystems;
using System;

namespace Project0
{
    [RequireComponent(typeof(EntityCreator))]
    public class KeyboardInput : MonoBehaviour
    {
#if UNITY_EDITOR
        GameEntity _entity;
        bool _removeDir;

        private void Start()
        {
            _entity = transform.GetComponent<EntityCreator>().entity;
        }

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                _removeDir = false;
                _entity.ReplaceDirection((Vector3.left + Vector3.up).normalized);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                _removeDir = false;
                _entity.ReplaceDirection((Vector3.left + Vector3.down).normalized);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                _removeDir = false;
                _entity.ReplaceDirection((Vector3.right + Vector3.down).normalized);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                _removeDir = false;
                _entity.ReplaceDirection((Vector3.right + Vector3.up).normalized);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _removeDir = false;
                _entity.ReplaceDirection(Vector3.right);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _removeDir = false;
                _entity.ReplaceDirection(Vector3.down);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _removeDir = false;
                _entity.ReplaceDirection(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                _removeDir = false;
                _entity.ReplaceDirection(Vector3.up);
            }
            else if (_removeDir && _entity.hasDirection)
            {
                _entity.RemoveDirection();
            }
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                _removeDir = true;
            }
        }
#endif
    }
}
