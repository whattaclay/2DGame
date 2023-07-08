using UnityEngine;

namespace EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        private Collider2D _collider;
        public static EnemyState EnemyState;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void TakeDamage(float damage)
        { 
            health -= damage;
            EnemyState = EnemyState.Hurt;
            if (health <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            EnemyState = EnemyState.Dead;
            _collider.enabled = false;
            enabled = false;
        }
    }
}