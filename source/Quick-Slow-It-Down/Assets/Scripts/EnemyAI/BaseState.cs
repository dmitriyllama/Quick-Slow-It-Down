namespace EnemyAI
{
    public abstract class BaseState
    {
        protected EnemyAI enemy;

        public BaseState(EnemyAI enemy)
        {
            this.enemy = enemy;
        }

        public abstract void Enter();
        public abstract void FixedUpdate();
        public abstract void Exit();
    }
}
