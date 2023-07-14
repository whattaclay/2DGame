using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyScripts
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyAnimation : MonoBehaviour
    {
        private Enemy _enemy;
        [SerializeField] private Animator animator;
        private static readonly int IsHurt = Animator.StringToHash("IsHurt");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");


        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }
        private void Update()
        {
            switch (_enemy.enemyState)
            {   
                case EnemyState.Attack:
                    animator.SetTrigger(IsAttack);
                    animator.SetBool(IsIdle, false);
                    animator.SetBool(IsWalking,false);
                    animator.SetBool(IsRunning, false);
                    _enemy.enemyState = EnemyState.Idle;
                    break;
                case EnemyState.Hurt:
                    animator.SetTrigger(IsHurt);
                    animator.SetBool(IsIdle, false);
                    _enemy.enemyState = EnemyState.Walk;
                    break;
                case EnemyState.Dead:
                    animator.SetBool(IsDead, true);
                    animator.SetBool(IsIdle, false);
                    break;
                case EnemyState.Walk:
                    animator.SetBool(IsIdle, false);
                    animator.SetBool(IsWalking,true);
                    animator.SetBool(IsRunning, false);
                    break;
                case EnemyState.Idle:
                    animator.SetBool(IsIdle, true);
                    animator.SetBool(IsWalking,false);
                    animator.SetBool(IsRunning, false);
                    break;
                case EnemyState.Run:
                    animator.SetBool(IsIdle, false);
                    animator.SetBool(IsRunning, true);
                    animator.SetBool(IsWalking,false);
                    break;
            }
        }
    }
}