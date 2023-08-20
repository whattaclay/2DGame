using Character.Attacks.DistanceAttack;
using Character.Attacks.MagicHit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class AttacksView : MonoBehaviour
    {
        [SerializeField] private MagicHit magicHit;
        [SerializeField] private DistanceAttack distanceAttack;
        [SerializeField] private TextMeshProUGUI hitCdView;
        [SerializeField] private TextMeshProUGUI powerFullHitCdView;
        [SerializeField] private TextMeshProUGUI magicShootCdView;
        [SerializeField] private Image hit;
        [SerializeField] private Image powerFullHit;
        [SerializeField] private Image shoot;
        private readonly Color _lowAlpha = new Color(1f, 1f, 1f, 0.4f);
        private readonly Color _fullAlpha = new Color(1f, 1f, 1f, 1f);

        private void Update()
        {
            hitCdView.text = magicHit.HitTimer.ToString("F1");
            powerFullHitCdView.text = magicHit.PowerFullTimer.ToString("F1");
            magicShootCdView.text = distanceAttack.ShootTimer.ToString("F1");
            
            hit.color = magicHit._isHit ? _lowAlpha : _fullAlpha;
            
            powerFullHit.color = magicHit._isPowerFullHit ? _lowAlpha : _fullAlpha;
            
            shoot.color = distanceAttack._isShoot ? _lowAlpha : _fullAlpha;
        }
    }
}