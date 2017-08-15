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
    public class TestController : Controller
    {
        protected override Systems GetSystems()
        {
            var contexts = Contexts.sharedInstance;
            return new Feature("Systems")
                .Add(new GodMovementSystem(contexts))
                .Add(new CameraFollowFigherSystem(contexts))
                .Add(new TransformSystems(contexts))
                .Add(new CleanupEmptyEntitiesSystem(contexts));
        }
    }
}