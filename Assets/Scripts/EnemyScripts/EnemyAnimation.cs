using UnityEngine;

namespace EnemyScripts
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int IsHurt = Animator.StringToHash("IsHurt");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        private void Update()
        {
            switch (Enemy.EnemyState)
            {
                case EnemyState.Hurt:
                    animator.SetTrigger(IsHurt);
                    Enemy.EnemyState = EnemyState.Idle;
                    break;
                case EnemyState.Dead:
                    animator.SetTrigger(IsDead);
                    Enemy.EnemyState = EnemyState.Idle;
                    break;
            }
        }
    }
}