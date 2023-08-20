using Sounds;
using UnityEngine;

namespace Character.Attacks.MagicHit
{
    public class MagicHit : MonoBehaviour
    {
        [SerializeField] private Transform hitPoint;
        [SerializeField] private float hitRange = 0.5f;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private float hitCd = 0.3f;
        [SerializeField] private float powerFullHitCd = 2f;
        [SerializeField] private float impulseMagnitude = 60f;
        [SerializeField] private AudioClip hit;
        [SerializeField] private AudioClip powerFullHit;
        public float HitTimer { get; private set; }
        public float PowerFullTimer { get; private set; }
        public bool _isHit { get; private set; }
        public bool _isPowerFullHit { get; private set; }
        private bool _readyToHit = true;
        private bool _readyToPowerFullHit = true;

        private void Awake()
        {
            HitTimer = hitCd;
            PowerFullTimer = powerFullHitCd;
        }

        private void Update()
        {
            PowerFullHitTimerLogic();
            HitTimerLogic();
        }
        private void PowerFullHitTimerLogic()
        {
            if (!_isPowerFullHit) return;
            PowerFullTimer -= Time.deltaTime;
            _readyToPowerFullHit = false;
            if (!(PowerFullTimer <= 0)) return;
            PowerFullTimer = powerFullHitCd;
            _isPowerFullHit = false;
            _readyToPowerFullHit = true;
        }
        private void HitTimerLogic()
        {
            if (!_isHit) return;
            HitTimer -= Time.deltaTime;
            _readyToHit = false;
            if (!(HitTimer <= 0)) return;
            HitTimer = hitCd;
            _isHit = false;
            _readyToHit = true;
        }
        public void Hit()
        {
            if (!_readyToHit) return;
            _isHit = true;
            PlayerAudioManager.instance.PlaySound(hit);
            CharacterController2D.FireState = FireState.MagicHit;
        }
        public void PowerFullHit()
        {
            if (!_readyToPowerFullHit) return;
            _isPowerFullHit = true;
            PlayerAudioManager.instance.PlaySound(powerFullHit);
            CharacterController2D.FireState = FireState.PowerFullMagicHit;
        }
        public void TakeDamage(float damage) //урон и импульс задается через аниматор
        {
            var hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, enemyLayers);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().GiveDamage(damage);
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2((hitPoint.position.x * impulseMagnitude),hitPoint.position.y),ForceMode2D.Impulse);
            }
        }
    }
}
