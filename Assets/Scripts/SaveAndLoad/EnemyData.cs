using System;
using EnemyScripts;

namespace SaveAndLoad
{
    [Serializable]
    public class EnemyData
    {
        public int[] aliveEnemies;
        public float[] enemyHealth;

        public EnemyData(EnemyManager manager)
        {
            aliveEnemies = new int[manager.enemies.Length];
            enemyHealth = new float[manager.enemies.Length];
            for (int i = 0; i < manager.enemies.Length; i++)
            {
                if (manager.enemies[i].gameObject.activeSelf)
                {
                    aliveEnemies[i] = 1;
                    enemyHealth[i] = manager.enemies[i].CurrentHealth;
                }
                else
                {
                    aliveEnemies[i] = 0;
                    enemyHealth[i] = 0;
                }
            }
        }
    }
}