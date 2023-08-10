using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyWanderState : EnemyState
    {
        public EnemyWanderState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
        }
        public override void EnterState()
        {
            base.EnterState();
            Enemy.EnemyWanderBaseInstance.DoEnterLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            Enemy.EnemyWanderBaseInstance.DoExitLogic();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            Enemy.EnemyWanderBaseInstance.DoFrameUpdateLogic();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Enemy.EnemyWanderBaseInstance.DoPhysicsLogic();
        }
    }
}
