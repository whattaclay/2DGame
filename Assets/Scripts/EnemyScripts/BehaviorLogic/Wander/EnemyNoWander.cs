using EnemyScripts.Base;
using EnemyScripts.BehaviorLogic.Idle;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Wander
{   
    [CreateAssetMenu(fileName = "Wander - No Wander",menuName = "Enemy Logic/Wander Logic/No Wander")]
    public class EnemyNoWander : EnemyWanderSOBase
    {
       public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
            Enemy.StateMachine.ChangeState(Enemy.IdleState);
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
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