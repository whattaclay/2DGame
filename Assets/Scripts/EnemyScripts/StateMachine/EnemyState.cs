using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.StateMachine
{
    public class EnemyState
    {
        protected Enemy enemy;
        protected EnemyStateMachine enemyStateMachine;

        protected EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
        {
            this.enemy = enemy;
            this.enemyStateMachine = enemyStateMachine;
        }
        public virtual void EnterState(){}
        
        public virtual void ExitState(){}
        
        public virtual void FrameUpdate(){}
        
        public virtual void PhysicsUpdate(){}
        
        public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType){}
    }
}