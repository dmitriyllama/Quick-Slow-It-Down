using UnityEngine;

namespace EnemyAI
{
    public class ShootingState : BaseState
    {
        public ShootingState(EnemyAI enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            enemy.SetRotationTarget(enemy.player.transform);
        }

        public override void FixedUpdate()
        {
            if (!enemy.alive) return;
            
            if (!enemy.player.alive)
            {
                // Killed the player!
                enemy.ForceChangeState(EnemyAI.State.Idle);
            }
            if (enemy.rotating)
            {
                enemy.Rotate();
            }
            else
            {
                var position = enemy.transform.position;
                var ray = new Ray(position, enemy.player.transform.position - position);
                if (Physics.Raycast(ray, out var hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (!hitObject.CompareTag("Player"))
                    {
                        // Lost player!
                        enemy.ChangeState(EnemyAI.State.Confused);
                    }
                }
                enemy.Aim(enemy.player.transform);
                enemy.Shoot();
            }
        }

        public override void Exit()
        {
        }
    }
}