using EnemyScripts.Base;
using TMPro;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Wander
{
    [CreateAssetMenu(fileName = "Wander - Wall To Wall Wander",menuName = "Enemy Logic/Wander Logic/Wall To Wall Wander")]
    public class EnemyWallToWallWander : EnemyWanderSOBase
    {
        [SerializeField] public float idleSpeed;
        private float _directionChanger;
        private Vector3 _direction;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
            _directionChanger = 1;
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
            Enemy.MoveEnemy(new Vector2( _directionChanger* idleSpeed, 0));
            if (Enemy.IsHitWall)
            {
                _directionChanger = -_directionChanger;
                Enemy.IsHitWall = false;
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