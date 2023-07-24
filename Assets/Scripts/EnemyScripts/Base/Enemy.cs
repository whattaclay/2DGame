using System;
using EnemyScripts.Interfaces;
using EnemyScripts.StateMachine;
using EnemyScripts.StateMachine.ConcreteState;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts.Base
{
    public class Enemy : MonoBehaviour , IDamageable , IEnemyMoveable, ITriggerCheckable
    {
        [field: SerializeField] public float MaxHealth { get; set; } = 100f;
        [field: SerializeField] public Animator Animator { get; set; }
        [field: SerializeField] public float CurrentHealth { get; set; }
        public Rigidbody2D Rb { get; set; }
        public bool IsFacingRight { get; set; } = true;
        public bool IsAggro { get; set; }
        public bool IsWithinAttackDistance { get; set; }

        #region Idle Variables

        public float randomMovementRange = 5f;
        public float idleSpeed;

        #endregion

        #region Chase Variables

        public float chaseSpeed;
        public float distanceToStopChasing;

        #endregion

        #region State Machine Variables

        public EnemyStateMachine StateMachine { get; set; }
        
        public EnemyWalkState WalkState { get; set; }
        
        public EnemyChaseState ChaseState { get; set; }

        public EnemyAttackState AttackState { get; set; }
        
        #endregion

        private void Awake()
        {
            StateMachine = new EnemyStateMachine();
            WalkState = new EnemyWalkState(this, StateMachine);
            ChaseState = new EnemyChaseState(this, StateMachine);
            AttackState = new EnemyAttackState(this, StateMachine);
        }
        private void Start()
        {
            CurrentHealth = MaxHealth;
            Rb = GetComponent<Rigidbody2D>();
            StateMachine.Initialize(WalkState);
        }
        private void Update()
        {
            StateMachine.CurrentEnemyState.FrameUpdate();
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentEnemyState.PhysicsUpdate();
        }
        #region Health / Die Functions
        
        public void Damage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }
        public void Die()
        {
            throw new System.NotImplementedException();
        }
        
        #endregion
        
        #region Movement Functions

        public void MoveEnemy(Vector2 velocity)
        {
            Rb.velocity = velocity;
            CheckForLeftOrRightFacing(velocity);
        }

        public void CheckForLeftOrRightFacing(Vector2 velocity)
        {
            if (IsFacingRight && velocity.x < 0f)
            {
                Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                IsFacingRight = !IsFacingRight;
            }
            else if (!IsFacingRight && velocity.x > 0f)
            {
                Vector3 rotator = new Vector3(transform.rotation.x, 0f , transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                IsFacingRight = !IsFacingRight;
            }
        }

        #endregion

        #region Animation Triggers

        private void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
        }
        public enum AnimationTriggerType
        {
            EnemyIdle,
            EnemyChase,
            EnemyAttack,
            EnemyDamaged
        }
        #endregion

        #region Distance Check

        public void SetAggroStatus(bool isAggro)
        {
            IsAggro = isAggro;
        }

        public void SetAttackDistanceBool(bool isWithinAttackDistance)
        {
            IsWithinAttackDistance = isWithinAttackDistance;
        }

        #endregion
    }
}
