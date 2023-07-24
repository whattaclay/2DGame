using System;
using Character;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] private float damage;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Player"))
                col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}