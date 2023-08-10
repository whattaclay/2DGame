﻿using Unity.VisualScripting;

namespace EnemyScripts.StateMachine
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentEnemyState { get; set; }

        public void Initialize(EnemyState startingState)
        {
            CurrentEnemyState = startingState;
            CurrentEnemyState.EnterState();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentEnemyState.ExitState();
            CurrentEnemyState = newState;
            CurrentEnemyState.EnterState();
        }
    }
}