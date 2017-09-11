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

namespace Project0.EntityCreators
{
    public class PlayerCreator : GameEntityCreator
    {
        public new Transform camera;
        public new Rigidbody rigidbody;
        public Animator animator;

        private void Awake()
        {
            entity = Contexts.sharedInstance.game.playerEntity;
            if (entity == null)
            {
                entity = Contexts.sharedInstance.game.CreateEntity();
                entity.isPlayer = true;
            }
            entity.AddTransform(transform);
            if (camera)
            {
                entity.AddCameraTransform(camera);
            }
            if (rigidbody)
            {
                entity.AddRigidbody(rigidbody);
            }
            if (animator)
            {
                entity.AddAnimator(animator);
            }
        }
    }
}