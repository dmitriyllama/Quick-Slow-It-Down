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
        }

        public override void Exit()
        {
        }
    }
}