using EnemyScripts.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyScripts.BehaviorLogic.Chase
{
    [CreateAssetMenu(fileName = "Chase - Direct Chase",menuName = "Enemy Logic/Chase Logic/Direct Chase")]
    public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
    {
        [SerializeField] private float chaseSpeed;
        [SerializeField] private float distanceToStopChasing;
        [SerializeField] private float timeToExitConfusion = 2f;
        
        private float _confusionTimer;
        
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        private static readonly int IsChasing = Animator.StringToHash("IsChasing");
        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
            Enemy.Animator.SetBool(IsChasing,true);
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
            Enemy.Animator.SetBool(IsChasing,false);
            Enemy.Animator.SetBool(IsIdle,false);
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
            Vector2 moveDirection = (PlayerTransform.position - Enemy.transform.position).normalized;
            Enemy.MoveEnemy( new Vector2(moveDirection.x * chaseSpeed, 0f));
            if (Enemy.IsWithinAttackDistance)
            {
                Enemy.StateMachine.ChangeState(Enemy.AttackState);
            }
            if (Vector2.Distance(PlayerTransform.position ,Enemy.transform.position ) > distanceToStopChasing || Enemy.IsHitWall)
            {
                _confusionTimer += Time.deltaTime;
                Enemy.Animator.SetBool(IsIdle,true);
                Enemy.MoveEnemy(Vector2.zero);
                if (_confusionTimer > timeToExitConfusion)
                {
                    Enemy.StateMachine.ChangeState(Enemy.WanderState);
                }
            }
            else
            {
                Enemy.Animator.SetBool(IsIdle,false);
                _confusionTimer = 0f;
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
