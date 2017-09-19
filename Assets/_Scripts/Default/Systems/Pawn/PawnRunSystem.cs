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
    public class PawnRunSystem : ReactiveSystem<GameEntity>
    {
        public PawnRunSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var pawn = e.GetPawnEntity();
                if (pawn != null && pawn.hasRigidbody && pawn.hasPosition)
                {
                    var speed = GameConfig.instance.pawnRunSpeed;
                    var pawnRB = pawn.rigidbody.value;
                    Vector3 dir = Vector3.zero;
                    if (e.hasDirection)
                    {
                        dir = e.direction.value;
                        dir.z = dir.y;
                        dir.y = 0f;
                        var camera = pawn.GetCameraEntity();
                        if (camera != null && camera.hasRotation)
                        {
                            dir = Quaternion.Euler(0f, camera.rotation.value.eulerAngles.y, 0f) * dir;
                        }
                        if (pawn.hasRotation)
                        {
                            var look = Quaternion.LookRotation(dir);
                            pawn.ReplaceRotation(look);
                        }
                        var dest = pawn.position.value + dir * speed / 10f;
                        pawnRB.position = dest;
                        RaycastHit hit;
                        if (Physics.Raycast(dest + Vector3.up / 4f, Vector3.down, out hit, 0.5f, GameConfig.instance.pawnTerrainMask & ~GameConfig.instance.pawnMask))
                        {
                            pawnRB.position = hit.point;
                        }
                    }
                    if (pawn.hasAnimator)
                    {
                        var anim = pawn.animator.value;
                        anim.SetBool("run", e.hasDirection);
                        anim.SetFloat("speed", speed);//pass
                    }
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasInputType && entity.inputType.value == InputType.TerrainMovement;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Direction.AddedOrRemoved());
        }
    }
}