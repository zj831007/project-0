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
using Project0.Design;

namespace Project0
{
    public class InputUtils
    {
        public static Vector3 GetTouchPosition(int id)
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#else
            return Input.touches[id].position;
#endif
        }
    }
}