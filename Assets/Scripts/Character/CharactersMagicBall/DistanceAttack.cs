using System.Collections;
using UnityEngine;

namespace Character.CharactersMagicBall
{
    public class DistanceAttack : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private float attackCd;
        [SerializeField] private float attackDelay;
        private bool _isAttackCd = false;
        private IEnumerator ReadinessToAttack() //делей к готовности атаковать
        {
            yield return new WaitForSeconds(attackDelay);
            Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation); //спавнит префаб шара в позицию выстрела
        } 
        public void Shoot() //запускает корутину выстрела
        {
            StartCoroutine(ReadinessToAttack());
            StartCoroutine(AttackCd());
        }
        public bool IsAttackCd() //публичная переменная, чтобы знать прошло ли кд после выстрела
        {
            return _isAttackCd;
        }
        private IEnumerator AttackCd() // делей после выстрела
        {
            _isAttackCd = true;
            yield return new WaitForSeconds(attackCd);
            _isAttackCd = false;
        }
    }
}
