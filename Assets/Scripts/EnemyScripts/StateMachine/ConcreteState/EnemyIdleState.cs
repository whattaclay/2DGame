using EnemyScripts.Base;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.EnemyIdleBaseInstance.DoEnterLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            Enemy.EnemyIdleBaseInstance.DoExitLogic();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            Enemy.EnemyIdleBaseInstance.DoFrameUpdateLogic();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Enemy.EnemyIdleBaseInstance.DoPhysicsLogic();
        }
    }
}