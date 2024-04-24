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
            if (!enemy.alive || !enemy.active) return;
            
            if (!enemy.player.alive)
            {
                // Killed the player!
                enemy.ChangeState(EnemyAI.State.Idle);
            }
            if (enemy.rotating)
            {
                enemy.Rotate();
            }
            else
            {
                enemy.Shoot();
            }
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}