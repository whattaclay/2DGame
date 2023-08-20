using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private Image currentHealthBar;
        [SerializeField] private TextMeshProUGUI healthAmount;
        private float _startHealth;
        
        void Start()
        {
            _startHealth = playerHealth.CurrentHealth;
            currentHealthBar.fillAmount = playerHealth.CurrentHealth;
        }
        void Update()
        {
            healthAmount.text = playerHealth.CurrentHealth.ToString(CultureInfo.InvariantCulture);
            currentHealthBar.fillAmount = playerHealth.CurrentHealth / _startHealth;
        }
    }
}
