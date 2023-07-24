using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyChaseState : EnemyState
    {
        private Transform _playerTransform;
        private float _timeToExitConfusion = 2f;
        private float _confusionTimer;
        
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        private static readonly int IsChasing = Animator.StringToHash("IsChasing");

        public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void EnterState()
        {
            base.EnterState();
            enemy.Animator.SetBool(IsChasing,true);
        }

        public override void ExitState()
        {
            base.ExitState();
            enemy.Animator.SetBool(IsChasing,false);
            enemy.Animator.SetBool(IsIdle,false);
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();

            Vector2 moveDirection = (_playerTransform.position - enemy.transform.position).normalized;
            enemy.MoveEnemy( new Vector2(moveDirection.x * enemy.chaseSpeed, 0f));
            if (enemy.IsWithinAttackDistance)
            {
                enemy.StateMachine.ChangeState(enemy.AttackState);
            }
            if (Vector2.Distance(_playerTransform.position ,enemy.transform.position ) > enemy.distanceToStopChasing)
            {
                _confusionTimer += Time.deltaTime;
                enemy.Animator.SetBool(IsIdle,true);
                enemy.MoveEnemy(Vector2.zero);
                if (_confusionTimer > _timeToExitConfusion)
                {
                    enemy.StateMachine.ChangeState(enemy.WalkState);
                }
            }
            else
            {
                enemy.Animator.SetBool(IsIdle,false);
                _confusionTimer = 0f;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
