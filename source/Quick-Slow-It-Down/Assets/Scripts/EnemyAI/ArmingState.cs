using System;
using UnityEngine;

namespace EnemyAI
{
    public class ArmingState : BaseState
    {
        public ArmingState(EnemyAI enemy) : base(enemy)
        {
            
        }

        public override void Enter()
        {
            enemy.SetRotationTarget(enemy.assignedGun.transform);
        }

        public override void FixedUpdate()
        {
            if (!enemy.alive) return;
            if (enemy.rotating)
            {
                enemy.Rotate();
            }
            else
            {
                var gunTransform = enemy.assignedGun.transform;
                var position = enemy.transform.position;
                if ((gunTransform.position - position).sqrMagnitude > 9)
                {
                    enemy.MoveToward(gunTransform);
                }
                else
                {
                    // Close enough to the gun!
                    enemy.ChangeState(EnemyAI.State.Shooting);
                }
            }
        }

        public override void Exit()
        {
            enemy.PickupGun();
        }
    }
}