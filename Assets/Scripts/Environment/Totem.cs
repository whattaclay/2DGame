using System;
using TMPro;
using UnityEngine;

namespace Environment
{
    public class Totem : MonoBehaviour
    {
        public Action OnActivated;
        [SerializeField] private GameObject glowElement;
        [SerializeField] private TextMeshProUGUI totemText;

        private void Awake()
        {
            totemText.enabled = false;
            glowElement.SetActive(false);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (totemText == null) return;
            if (!other.CompareTag("Player")) return;
            totemText.enabled = true;
            if (Input.GetKey(KeyCode.R) && glowElement.activeSelf == false)
            {
                Activate();
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (totemText == null) return;
            if (!other.CompareTag("Player")) return;
            totemText.enabled = false;
        }
        private void Activate()
        {
            Destroy(totemText);
            glowElement.SetActive(true);
            OnActivated?.Invoke();
        }
    }
}