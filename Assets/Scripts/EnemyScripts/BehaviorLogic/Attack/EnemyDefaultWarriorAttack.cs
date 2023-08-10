using System.Collections;
using Character;
using EnemyScripts.Base;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyScripts.BehaviorLogic.Attack
{
    [CreateAssetMenu(fileName = "Attack - Default Warrior Attack",menuName = "Enemy Logic/Attack Logic/Default Warrior Attack")]
    public class EnemyDefaultWarriorAttack : EnemyAttackSOBaase
    {
        [SerializeField] private float timeBetweenHits = 1f;
        [SerializeField] private float timeTillExit = 1f;
        [SerializeField] private float distanceToCountExit = 1f;
        [SerializeField] private  float animationDelay = 0.5f;

        private float _exitTimer;
        private float _timer = 0.75f;
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");

        
        private float _animationTimer;
        private bool _readyToGiveDamage;


        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
            Enemy.Animator.SetBool(IsIdle, true);
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
            Enemy.Animator.SetBool(IsIdle, false);
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
            Enemy.MoveEnemy(Vector2.zero);
            if (_readyToGiveDamage)
            {
                _animationTimer += Time.deltaTime;
                if (_animationTimer > animationDelay)
                {
                    _animationTimer = 0f;
                    _readyToGiveDamage = false;
                    Enemy.GiveDamage();
                }
            }
            if (_timer > timeBetweenHits)
            {
                Enemy.Animator.SetBool(IsIdle,false);
                Enemy.Animator.SetTrigger(IsAttack);
                _readyToGiveDamage = true;
                _timer = 0f;
            }
            else
            {
                Enemy.Animator.SetBool(IsIdle,true);
            }
            if (Vector2.Distance(PlayerTransform.position, Enemy.transform.position)> distanceToCountExit)
            {
                _readyToGiveDamage = false;
                _timer = 0f;
                _exitTimer += Time.deltaTime;
                if (_exitTimer > timeTillExit)
                {
                    Enemy.StateMachine.ChangeState(Enemy.ChaseState);
                }
            }
            else
            {
                _timer += Time.deltaTime;
                _exitTimer = 0;
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
