using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.EnemyAttackBaseInstance.DoEnterLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            Enemy.EnemyAttackBaseInstance.DoExitLogic();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            Enemy.EnemyAttackBaseInstance.DoFrameUpdateLogic();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Enemy.EnemyAttackBaseInstance.DoPhysicsLogic();
        }
    }
}
