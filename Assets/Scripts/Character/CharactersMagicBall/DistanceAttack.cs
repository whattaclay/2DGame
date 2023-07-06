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
        private CharacterController2D _characterController2D;
        private bool _isAttackCd;

        private void Awake()
        {
            _characterController2D = GetComponent<CharacterController2D>();
        }
        public void Shoot() 
        {
            if(_isAttackCd)return;
            _characterController2D.fireState = FireState.Fire1;
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
