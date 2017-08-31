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
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            RaycastHit hit;
            if (Physics.Raycast(Vector3.up, -Vector3.up, out hit, 4))
            {
            }
        }
    }
}