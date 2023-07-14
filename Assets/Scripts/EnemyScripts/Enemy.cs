using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        private Collider2D _collider;
        public  EnemyState enemyState;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void GiveDamage()
        {
            Debug.Log("GiveDamage");
        }
        public void TakeDamage(float damage)
        { 
            health -= damage;
            enemyState = EnemyState.Hurt;
            if (health <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            _rb.velocity = Vector2.zero;
            enemyState = EnemyState.Dead;
            _collider.enabled = false;
            enabled = false;
            _rb.bodyType = RigidbodyType2D.Kinematic;
        }
        public void DestroyBody()
        {
            Destroy(gameObject);  
        }
    }
}