using System.Collections;
using Character;
using EnemyScripts.BehaviorLogic.Attack;
using EnemyScripts.BehaviorLogic.Chase;
using EnemyScripts.BehaviorLogic.Die;
using EnemyScripts.BehaviorLogic.Idle;
using EnemyScripts.BehaviorLogic.Wander;
using EnemyScripts.Interfaces;
using EnemyScripts.StateMachine;
using EnemyScripts.StateMachine.ConcreteState;
using UnityEngine;
using EnemyIdleState = EnemyScripts.StateMachine.ConcreteState.EnemyIdleState;

namespace EnemyScripts.Base
{
    public class Enemy : MonoBehaviour , IDamageable , IEnemyMoveable, ITriggerCheckable
    {
        [field: SerializeField] public float MaxHealth { get; set; } = 100f;
        [field: SerializeField] public Animator Animator { get; set; }
        [field: SerializeField] public float CurrentHealth { get; set; }
        [SerializeField] private float attackDamage;
        public Rigidbody2D Rb { get; set; }
        public bool IsFacingRight { get; set; } = true;
        public bool IsAggro { get; set; }
        public bool IsWithinAttackDistance { get; set; }
        
        public bool IsHitWall { get; set; }
        public Collider2D[] Colliders { get; private set; }
        private Health _playerHealth;

        #region Animation

        private static readonly int IsHurt = Animator.StringToHash("IsHurt");

        #endregion

        #region ScriptableOdject Variables

        [SerializeField] private EnemyIdleSOBase enemyIdleBase;
        [SerializeField] private EnemyWanderSOBase enemyWanderBase;
        [SerializeField] private EnemyChaseSOBase enemyChaseBase;
        [SerializeField] private EnemyAttackSOBaase enemyAttackBase;
        [SerializeField] private EnemyDieSOBase enemyDieBase;

        public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
        public EnemyWanderSOBase EnemyWanderBaseInstance { get; set; }
        public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
        public EnemyAttackSOBaase EnemyAttackBaseInstance { get; set; }
        public EnemyDieSOBase EnemyDieBaseInstance { get; set; }

        #endregion
        
        #region State Machine Variables

        public EnemyStateMachine StateMachine { get; set; }
        public EnemyIdleState IdleState { get; set; }
        public EnemyWanderState WanderState { get; set; }
        public EnemyChaseState ChaseState { get; set; }
        public EnemyAttackState AttackState { get; set; }
        public EnemyDieState DieState { get; set; }
        
        #endregion

        private void Awake()
        {
            EnemyIdleBaseInstance = Instantiate(enemyIdleBase);
            EnemyWanderBaseInstance = Instantiate(enemyWanderBase);
            EnemyChaseBaseInstance = Instantiate(enemyChaseBase);
            EnemyAttackBaseInstance = Instantiate(enemyAttackBase);
            EnemyDieBaseInstance = Instantiate(enemyDieBase);
            
            StateMachine = new EnemyStateMachine();

            IdleState = new EnemyIdleState(this, StateMachine);
            WanderState = new EnemyWanderState(this, StateMachine);
            ChaseState = new EnemyChaseState(this, StateMachine);
            AttackState = new EnemyAttackState(this, StateMachine);
            DieState = new EnemyDieState(this, StateMachine);
            
            Colliders = GetComponents<Collider2D>();
            
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
        private void Start()
        {
            CurrentHealth = MaxHealth;
            Rb = GetComponent<Rigidbody2D>();
            
            EnemyIdleBaseInstance.Initialize(gameObject,this);
            EnemyWanderBaseInstance.Initialize(gameObject, this);
            EnemyChaseBaseInstance.Initialize(gameObject, this);
            EnemyAttackBaseInstance.Initialize(gameObject, this);
            EnemyDieBaseInstance.Initialize(gameObject,this);
            
            StateMachine.Initialize(WanderState);
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
            Animator.SetTrigger(IsHurt);
            MoveEnemy(Vector2.zero);
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }
        public void Die()
        {
            StateMachine.ChangeState(DieState);
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
            switch (IsFacingRight)
            {
                case true when velocity.x < 0f:
                {
                    var rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                    transform.rotation = Quaternion.Euler(rotator);
                    IsFacingRight = !IsFacingRight;
                    break;
                }
                case false when velocity.x > 0f:
                {
                    var rotator = new Vector3(transform.rotation.x, 0f , transform.rotation.z);
                    transform.rotation = Quaternion.Euler(rotator);
                    IsFacingRight = !IsFacingRight;
                    break;
                }
            }
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

        public void GiveDamage()
        {
            _playerHealth.TakeDamage(attackDamage);
        }
    }
}
