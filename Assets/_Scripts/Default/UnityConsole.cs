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
    public class UnityConsole : Singleton<UnityConsole>
    {
        public Transform content;
        public ScrollRect scrollRect;
        public GameObject text;
        public bool debug = true;

        public void Log<T>(T msg)
        {
            if (debug)
            {
                var newText = Instantiate(text) as GameObject;
                newText.GetComponent<Text>().text = msg.ToString();
                newText.transform.SetParent(content);
                Canvas.ForceUpdateCanvases();
                scrollRect.verticalNormalizedPosition = 0;
            }
        }
    }
}