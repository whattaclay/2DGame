using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Die
{
    [CreateAssetMenu(fileName = "Die - Die After Timer",menuName = "Enemy Logic/Die Logic/Die After Timer")]
    public class EnemyDieAfterTimer : EnemyDieSOBase
    {
        [SerializeField] private float destroyAfterTime = 5f;
        private float _timer;
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
            Enemy.Animator.SetBool(IsDead,true);
            Enemy.Rb.isKinematic = true;
            foreach (var col in Enemy.Colliders)
            {
                col.enabled = false;
            }
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
            _timer += Time.deltaTime;
            if (_timer >= destroyAfterTime)
            {
                Enemy.gameObject.SetActive(false);
            }
        }

        public override void DoPhysicsLogic()
        {
            base.DoPhysicsLogic();
        }
    }
}