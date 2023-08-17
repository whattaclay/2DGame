using EnemyScripts.Base;
using UnityEngine;

namespace EnemyScripts.EnemyTypes
{
    public class DarkWizard : Enemy
    {
        [Header("Shoot Settings")]
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject ballPrefab;
        public void ThrowMagicBall()
        {
            Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}