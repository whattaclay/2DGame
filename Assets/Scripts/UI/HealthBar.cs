using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private Image currentHealthBar;
        private float _startHealth;

        // Start is called before the first frame update
        void Start()
        {
            _startHealth = playerHealth.CurrentHealth;
            currentHealthBar.fillAmount = playerHealth.CurrentHealth;
        }

        // Update is called once per frame
        void Update()
        {
            currentHealthBar.fillAmount = playerHealth.CurrentHealth / _startHealth;
        }
    }
}
