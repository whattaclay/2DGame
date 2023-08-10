using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.TriggerChecks
{
    public class EnemyWallCheck : MonoBehaviour
    {
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponentInParent<Enemy>();
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Environment"))
            {
                _enemy.IsHitWall = true;
            }
        }
    }
}