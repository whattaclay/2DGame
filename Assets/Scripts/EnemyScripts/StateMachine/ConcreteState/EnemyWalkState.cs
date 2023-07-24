using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.StateMachine.ConcreteState
{
    public class EnemyWalkState : EnemyState
    {
        private Vector3 _targetPosition;
        private Vector3 _direction;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public EnemyWalkState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            _targetPosition = GetRandomPointToMove();
            enemy.Animator.SetBool(IsWalking,true);
        }

        public override void ExitState()
        {
            base.ExitState();
            enemy.Animator.SetBool(IsWalking,false);
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            if (enemy.IsAggro)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
            _direction = (_targetPosition - enemy.transform.position).normalized;
            enemy.MoveEnemy(_direction * enemy.idleSpeed);
            if ((enemy.transform.position - _targetPosition).sqrMagnitude < 0.01f)
            {
                _targetPosition = GetRandomPointToMove();
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);
        }

        private Vector3 GetRandomPointToMove()
        {
            return enemy.transform.position + new Vector3(Random.Range(-enemy.randomMovementRange,enemy.randomMovementRange),0f,0f);
        }
    }
}
