using System.Collections;
using EnemyScripts;
using UnityEngine;

namespace Character.Attacks.MagicHit
{
    public class MagicHit : MonoBehaviour
    {
        [SerializeField] private Transform hitPoint;
        [SerializeField] private float hitRange = 0.5f;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private float hitDamage = 10f;
        [SerializeField] private float hitCd = 0.3f;
        [SerializeField] private float powerFullHitDamage = 15f;
        [SerializeField] private float powerFullHitCd = 2f;
        private bool _readyToHit = true;
        private bool _readyToPowerFullHit = true;
        private const float PowerFullHitDelay = 0.25f;
        public void Hit()
        {
            if (!_readyToHit) return;
            CharacterController2D.FireState = FireState.MagicHit;
            _readyToHit = false;
            StartCoroutine(HitDelay());
            TakeDamage(hitDamage);
        }
        public void PowerFullHit()
        {
            if (!_readyToPowerFullHit) return;
            CharacterController2D.FireState = FireState.PowerFullMagicHit;
            _readyToPowerFullHit = false;
            StartCoroutine(PowerFullHitCd());
            StartCoroutine(HitAfter());
        }
        private void TakeDamage(float damage)
        {
            var hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, enemyLayers);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
            /*Collider2D[] colliders = new Collider2D[10];
            var value = Physics2D.OverlapCircleNonAlloc(hitPoint.position, hitRange, colliders, enemyLayers);
            foreach (var enemy in colliders)
            {
                if (value > 0)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(hitDamage);
                }
            }*/
        }

        private IEnumerator HitAfter()
        {
            yield return new WaitForSeconds(PowerFullHitDelay);
            TakeDamage(powerFullHitDamage);
        }
        private IEnumerator HitDelay()
        {
            yield return new WaitForSeconds(hitCd);
            _readyToHit = true;
        }
        private IEnumerator PowerFullHitCd()
        {
            yield return new WaitForSeconds(powerFullHitCd);
            _readyToPowerFullHit = true;
        }
    }
}
