using UnityEngine;

namespace EnemyAI
{
    public class IdleState : BaseState
    {
        public IdleState(EnemyAI enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            // Does nothing
        }

        public override void FixedUpdate()
        {
            if (!enemy.alive) return;
            
            var position = enemy.transform.position;
            var ray = new Ray(position, enemy.playerTransform.position - position);
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
            // Does nothing
        }
    }
}