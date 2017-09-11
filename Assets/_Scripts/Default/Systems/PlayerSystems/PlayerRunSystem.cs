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
    public class PlayerRunSystem : ReactiveSystem<InputEntity>
    {
        GameContext _game;

        public PlayerRunSystem(Contexts contexts) : base(contexts.input)
        {
            _game = contexts.game;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var joystick = entities[0];
            var player = _game.playerEntity;
            if (player != null && player.hasTransform && player.hasRigidbody && player.hasCameraTransform)
            {
                var speed = GameConfig.instance.playerRunSpeed;
                var playerBody = player.rigidbody.value;
                var playerTransform = player.transform.value;
                var dir = joystick.hasDirection ? joystick.direction.value : Vector3.zero;
                if (dir != Vector3.zero)
                {
                    dir.z = dir.y;
                    dir.y = 0f;
                    var angles = player.cameraTransform.value.eulerAngles;
                    var rotation = Quaternion.Euler(0f, angles.y, 0f);
                    dir = rotation * dir;
                    var dest = playerTransform.position + dir * speed * Time.deltaTime;
                    playerBody.MoveRotation(Quaternion.LookRotation(dir));
                    RaycastHit _hit;
                    if (Physics.Raycast(dest + Vector3.up, Vector3.down, out _hit, 1.5f, GameConfig.instance.playerTerrainMask & ~GameConfig.instance.playerMask))//pass
                    {
                        dest.y = _hit.point.y;
                        dir = (dest - playerTransform.position).normalized;
                        dest = playerTransform.position + dir * speed * Time.deltaTime;
                    }
                    playerBody.MovePosition(dest);
                }
                if (player.hasAnimator)
                {
                    var playerAnim = player.animator.value;
                    playerAnim.SetBool("run", joystick.hasDirection);
                    playerAnim.SetFloat("speed", speed / 5f);//pass
                }
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return /*entity.hasDirection &&*/ entity.hasName && entity.name.value == "LeftJoystick";
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Direction.AddedOrRemoved());
        }
    }
}