using TMPro;
using UnityEngine;

namespace Environment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI portalText;
        [SerializeField] private Transform tpPoint;
        private Transform _player;

        private void Awake()
        {
            portalText.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            _player = col.GetComponent<Transform>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            portalText.enabled = true;
            if (Input.GetKey(KeyCode.R))
            {
                _player.position = tpPoint.position;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            portalText.enabled = false;
        }
    }
}