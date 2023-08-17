using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Environment
{
    public class HealthCrystal : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI crystalText;
        [SerializeField] private TextMeshProUGUI applyHealthText;
        [SerializeField] private float applyHealth;
        
        [Header("Эффект от использования")]
        [SerializeField] private GameObject effectPrefab;

        private void Awake()
        {
            applyHealthText.text = "+" + applyHealth.ToString(CultureInfo.InvariantCulture);
            crystalText.enabled = false;
            applyHealthText.enabled = false;
        }
        private void OnTriggerStay2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            crystalText.enabled = true;
            applyHealthText.enabled = true;
            if (!Input.GetKey(KeyCode.R)) return;
            Instantiate(effectPrefab, col.transform.position, col.transform.rotation)
                .GetComponent<HealthCrystalEffect>()
                .SetBodyToFollow(col.GetComponent<Health>(),applyHealth);
            Destroy(gameObject);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.CompareTag("Player")) return;
            crystalText.enabled = false;
            applyHealthText.enabled = false;
        }
    }
}