using EnemyScripts.Base;
using EnemyScripts.BehaviorLogic.Die;
using UnityEngine;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyDieState : EnemyState
    {
        
        public EnemyDieState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.EnemyDieBaseInstance.DoEnterLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            Enemy.EnemyDieBaseInstance.DoExitLogic();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            Enemy.EnemyDieBaseInstance.DoFrameUpdateLogic();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Enemy.EnemyDieBaseInstance.DoPhysicsLogic();
        }
    }
}