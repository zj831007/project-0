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

namespace Project0
{
    public enum InputMode
    {
        Normal, God
    };
    public class InputActivity : DictionaryAsset
    {
        public InputMode mode;
        public bool LeftJoystick;
        public bool RightJoystick;
        public bool LeftTupleButton;
        public bool RightPad;
    }
}