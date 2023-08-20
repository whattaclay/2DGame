using Sounds;
using UnityEngine;

namespace Character.Attacks.DistanceAttack
{
    public class DistanceAttack : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private float attackCd;
        [SerializeField] private AudioClip shoot;
        public float ShootTimer { get; private set; }
        private bool _isAttackCd;
        public bool _isShoot { get; private set; }

        private void Awake()
        {
            ShootTimer = attackCd;
        }

        private void Update()
        {
            if (_isShoot)
            {
                ShootTimer -= Time.deltaTime;
                _isAttackCd = true;
                if (!(ShootTimer <= 0)) return;
                _isShoot = false;
                ShootTimer = attackCd;
                _isAttackCd = false;
            }
        }
        public void Shoot()
        {
            if(_isAttackCd)return;
            _isShoot = true;
            PlayerAudioManager.instance.PlaySound(shoot);
            CharacterController2D.FireState = FireState.DistanceAttack;
        }
        public void InstanceBallPrefab() //префаб спавнится через аниматор
        {
            Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation); 
        }
    }
}
