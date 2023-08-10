using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyChaseState : EnemyState
    {
        public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.EnemyChaseBaseInstance.DoEnterLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            Enemy.EnemyChaseBaseInstance.DoExitLogic();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            Enemy.EnemyChaseBaseInstance.DoFrameUpdateLogic();
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Enemy.EnemyChaseBaseInstance.DoPhysicsLogic();
        }
    }
}
