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
    public class CleanupEmptyEntitiesSystem : ICleanupSystem
    {
        Contexts _context;

        public CleanupEmptyEntitiesSystem(Contexts contexts)
        {
            _context = contexts;
        }

        public void Cleanup()
        {
            foreach (var e in _context.game.GetEntities())
            {
                if (e.GetComponentIndices().Length == 0)
                {
                    e.Destroy();
                }
            }
        }
    }
}