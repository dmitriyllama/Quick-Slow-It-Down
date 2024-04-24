using UnityEngine;

namespace EnemyAI
{
    public class ConfusedState : BaseState
    {
        public ConfusedState(EnemyAI enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            enemy.LookRandomly();
        }

        public override void FixedUpdate()
        {
            if (!enemy.alive) return;
         
            enemy.Rotate();
            
            var position = enemy.transform.position;
            var ray = new Ray(position, enemy.player.transform.position - position);
            if (Physics.Raycast(ray, out var hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Player"))
                {
                    // Saw player!
                    enemy.ChangeState(EnemyAI.State.Arming);
                }
            }
        }

        public override void Exit()
        {
        }
    }
}