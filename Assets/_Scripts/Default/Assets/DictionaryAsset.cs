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
    public class DictionaryAsset : ScriptableObject
    {
        Type _self;

        public object this[string field]
        {
            get
            {
                if (_self == null) _self = GetType();
                return _self.GetField(field).GetValue(this);
            }
            set
            {
                if (_self == null) _self = GetType();
                _self.GetField(field).SetValue(this, value);
            }
        }
    }
}