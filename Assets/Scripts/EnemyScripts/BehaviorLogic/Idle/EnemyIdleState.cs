using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Idle
{
    [CreateAssetMenu(fileName = "Idle - Idle",menuName = "Enemy Logic/Idle Logic/Idle")]
    public class EnemyIdleState : EnemyIdleSOBase
    {
        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFrameUpdateLogic()
        {
            Enemy.MoveEnemy(Vector2.zero);
            base.DoFrameUpdateLogic();
            if (Enemy.IsAggro)
            {
                Enemy.StateMachine.ChangeState(Enemy.AttackState);
            }
        }

        public override void DoPhysicsLogic()
        {
            base.DoPhysicsLogic();
        }

        public override void ResetLogic()
        {
            base.ResetLogic();
        }
    }
}