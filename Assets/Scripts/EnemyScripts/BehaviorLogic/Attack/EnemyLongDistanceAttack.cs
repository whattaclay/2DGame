using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Attack
{
    [CreateAssetMenu(fileName = "Attack - Long Distance Attack",menuName = "Enemy Logic/Attack Logic/Long Distance Attack")]
    public class EnemyLongDistanceAttack : EnemyAttackSOBaase
    {
        [SerializeField] private float timeBetweenHits = 1f;
        [SerializeField] private float timeTillExit = 1f;
        [SerializeField] private float distanceToCountExit = 1f;
        private float _exitTimer;
        private float _timer = 0.75f;
        private static readonly int IsShoot = Animator.StringToHash("IsShoot");


        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
            Enemy.MoveEnemy(Vector2.zero);
            if (_timer > timeBetweenHits)
            {
                Enemy.Animator.SetTrigger(IsShoot);
                _timer = 0f;
            }
            if (Vector2.Distance(PlayerTransform.position, Enemy.transform.position)> distanceToCountExit)
            {
                _timer = 0f;
                _exitTimer += Time.deltaTime;
                if (_exitTimer > timeTillExit)
                {
                    Enemy.StateMachine.ChangeState(Enemy.IdleState);
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