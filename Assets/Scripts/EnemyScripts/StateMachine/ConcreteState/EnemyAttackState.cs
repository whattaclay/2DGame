using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyAttackState : EnemyState
    {
        private Transform _playerTransform;
        private float _timer = 0.5f;
        private float _timeBetweenHits = 1f;

        private float _exitTimer;
        private float _timeTillExit = 1f;
        private float _distanceToCountExit = 1f;
        
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");


        public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void EnterState()
        {
            base.EnterState();
            enemy.Animator.SetBool(IsIdle, true);
        }

        public override void ExitState()
        {
            base.ExitState();
            enemy.Animator.SetBool(IsIdle, false);
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            
            enemy.MoveEnemy(Vector2.zero);
            if (_timer > _timeBetweenHits)
            {
                _timer = 0f;
                enemy.Animator.SetTrigger(IsAttack);
            }

            if (Vector2.Distance(_playerTransform.position, enemy.transform.position)> _distanceToCountExit)
            {
                _exitTimer += Time.deltaTime;
                if (_exitTimer > _timeTillExit)
                {
                    enemy.StateMachine.ChangeState(enemy.ChaseState);
                }
            }
            else
            {
                _exitTimer = 0;
            }
            _timer += Time.deltaTime;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
