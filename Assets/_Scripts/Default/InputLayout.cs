using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Object = UnityEngine.Object;
using System.Linq;

namespace Project0
{
    public class InputLayout : MonoBehaviour
    {
        public static InputMode inputMode
        {
            get { return _activeMode; }
            set
            {
                foreach (var creator in _allCreators)
                {
                    creator.entity.isInactive = true;
                }
                if (_layouts[value]._creators != null)
                {
                    foreach (var creator in _layouts[value]._creators)
                    {
                        if (creator)
                        {
                            creator.entity.isInactive = false;
                        }
                    }
                }
                _activeMode = value;
            }
        }
        static InputMode _activeMode;
        static Dictionary<InputMode, InputLayout> _layouts = new Dictionary<InputMode, InputLayout>();
        static EntityCreator[] _allCreators;

        [SerializeField]
        InputMode _mode;
        [SerializeField]
        EntityCreator[] _creators;
        private void Awake()
        {
            _layouts[_mode] = this;
            GameInitSystem.OnInit += OnInit;
        }
        void OnInit()
        {
            _allCreators = Array.FindAll(FindObjectsOfType<EntityCreator>(), c => c.entity.hasInputType);
            inputMode = GameConfig.instance.inputMode;
        }
    }
}