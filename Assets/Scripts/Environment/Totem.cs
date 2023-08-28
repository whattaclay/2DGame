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
        public bool isActivated;


        private void Awake()
        {
            totemText.enabled = false;
            glowElement.SetActive(false);
        }
        private void Update() //код для сохранения прогресса
        {
            if (!isActivated && totemText) return;
            glowElement.SetActive(true);
            Destroy(totemText);
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
            glowElement.SetActive(true);
            isActivated = true;
            Destroy(totemText);
            OnActivated?.Invoke();
        }
    }
}