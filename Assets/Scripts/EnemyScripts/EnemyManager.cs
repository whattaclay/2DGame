using UnityEngine;

namespace EnemyScripts
{
    public class EnemyManager : MonoBehaviour
    {
        public Health[] enemies;

        private void Awake()
        {
            enemies = GetComponentsInChildren<Health>();
        }
    }
}