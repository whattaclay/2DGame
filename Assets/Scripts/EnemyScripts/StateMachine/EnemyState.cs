using EnemyScripts.Base;

namespace EnemyScripts.StateMachine
{
    public class EnemyState
    {
        protected Enemy Enemy;
        protected EnemyStateMachine EnemyStateMachine;

        protected EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
        {
            this.Enemy = enemy;
            this.EnemyStateMachine = enemyStateMachine;
        }
        public virtual void EnterState(){}
        
        public virtual void ExitState(){}
        
        public virtual void FrameUpdate(){}
        
        public virtual void PhysicsUpdate(){}
       
    }
}