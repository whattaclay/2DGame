using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Idle
{
    // ReSharper disable once InconsistentNaming
    public class EnemyIdleSOBase : ScriptableObject
    {
        protected Enemy Enemy;
        protected Transform Transform;
        protected GameObject GameObject;

        protected Transform PlayerTransform;

        public virtual void Initialize(GameObject gameObject, Enemy enemy)
        {
            this.GameObject = gameObject;
            Transform = gameObject.transform;
            this.Enemy = enemy;

            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public virtual void DoEnterLogic(){}
        
        public virtual void DoExitLogic() { ResetLogic(); }

        public virtual void DoFrameUpdateLogic()
        {
            if (Enemy.IsAggro)
            {
                Enemy.StateMachine.ChangeState(Enemy.AttackState);
            }
        }
        
        public virtual void DoPhysicsLogic(){}
        
        public virtual void ResetLogic(){}
    }
}