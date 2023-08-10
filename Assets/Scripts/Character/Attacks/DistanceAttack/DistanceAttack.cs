using System.Collections;
using UnityEngine;

namespace Character.Attacks.DistanceAttack
{
    public class DistanceAttack : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private float attackCd;
        [SerializeField] private float attackDelay;
        private bool _isAttackCd;
        public void Shoot()
        {
            if(_isAttackCd)return;
            CharacterController2D.FireState = FireState.DistanceAttack;
            StartCoroutine(ReadinessToAttack());
            StartCoroutine(AttackCd());
        }
        private IEnumerator ReadinessToAttack() 
        {
            yield return new WaitForSeconds(attackDelay);
            Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation); 
        } 
        private IEnumerator AttackCd() 
        {
            _isAttackCd = true;
            yield return new WaitForSeconds(attackCd);
            _isAttackCd = false;
        }
    }
}
