using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Die
{
    // ReSharper disable once InconsistentNaming
    public class EnemyDieSOBase : ScriptableObject
    {
        protected Enemy Enemy;
        protected Transform Transform;
        protected GameObject GameObject;

        public virtual void Initialize(GameObject gameObject, Enemy enemy)
        {
            this.GameObject = gameObject;
            Transform = gameObject.transform;
            this.Enemy = enemy;
        }
        public virtual void DoEnterLogic(){}
        
        public virtual void DoExitLogic() { ResetLogic(); }

        public virtual void DoFrameUpdateLogic() { }
        
        public virtual void DoPhysicsLogic(){}
        
        public virtual void ResetLogic(){}
    }
}