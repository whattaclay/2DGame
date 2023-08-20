using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.TriggerChecks
{
    public class EnemyAttackDistanceCheck : MonoBehaviour
    {
        public GameObject PlayerTarget { get; set; }
        private Enemy _enemy;

        private void Awake()
        {
            PlayerTarget = GameObject.FindGameObjectWithTag("Player");
            _enemy = GetComponentInParent<Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject == PlayerTarget)
            {
                _enemy.SetAttackDistanceBool(true);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject == PlayerTarget)
            {
                _enemy.SetAttackDistanceBool(false);
            }
        }
    }
}
