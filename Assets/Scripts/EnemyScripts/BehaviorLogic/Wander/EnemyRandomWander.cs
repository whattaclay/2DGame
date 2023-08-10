using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Wander
{
    [CreateAssetMenu(fileName = "Wander - Random Wander",menuName = "Enemy Logic/Wander Logic/Random Wander")]
    public class EnemyRandomWander : EnemyWanderSOBase
    {
        [SerializeField] public float randomMovementRange = 5f;
        [SerializeField] public float idleSpeed;
        
        private Vector3 _targetPosition;
        private Vector3 _direction;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
            _targetPosition = GetRandomPointToMove();
            Enemy.Animator.SetBool(IsWalking,true);
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
            Enemy.Animator.SetBool(IsWalking,false);
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
            if (Enemy.IsAggro)
            {
                Enemy.StateMachine.ChangeState(Enemy.ChaseState);
            }
            _direction = (_targetPosition - Enemy.transform.position).normalized;
            Enemy.MoveEnemy(new Vector2(_direction.x * idleSpeed, 0));
            if ((Enemy.transform.position - _targetPosition).sqrMagnitude < 0.01f)
            {
                _targetPosition = GetRandomPointToMove();
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
        
        private Vector3 GetRandomPointToMove()
        {
            return Enemy.transform.position + new Vector3(Random.Range(-randomMovementRange,randomMovementRange),0f,0f);
        }
    }
}
