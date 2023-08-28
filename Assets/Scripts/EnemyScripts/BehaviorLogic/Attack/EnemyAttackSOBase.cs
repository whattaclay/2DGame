using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Attack
{
    // ReSharper disable once InconsistentNaming
    public class EnemyAttackSOBase : ScriptableObject
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
        
        public virtual void DoExitLogic(){ ResetLogic(); }

        public virtual void DoFrameUpdateLogic()
        {
            if (PlayerTransform.position.x > Transform.position.x && !Enemy.IsFacingRight)
            {
                Enemy.IsFacingRight = !Enemy.IsFacingRight;
                var rotator = new Vector3(Transform.rotation.x, 0f, Transform.rotation.z);
                Transform.rotation = Quaternion.Euler(rotator);
            }
            else if(PlayerTransform.position.x < Transform.position.x && Enemy.IsFacingRight)
            {
                Enemy.IsFacingRight = !Enemy.IsFacingRight;
                var rotator = new Vector3(Transform.rotation.x, 180f, Transform.rotation.z);
                Transform.rotation = Quaternion.Euler(rotator);
            }
        }
        
        public virtual void DoPhysicsLogic(){}
        
        public virtual void ResetLogic(){}
    }
}
