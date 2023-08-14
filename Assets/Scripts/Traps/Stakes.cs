using Character;
using UnityEngine;

namespace Traps
{
    public class Stakes : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform leftTpPoint;
        [SerializeField] private Transform rightTpPoint;
        private GameObject _player;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            _player = col.gameObject;
            _player.GetComponent<Health>().TakeDamage(damage);
            if (_player.GetComponent<Health>().CurrentHealth > 0)
            {
                _player.transform.position =
                    transform.position.x - _player.transform.position.x > 0 
                        ? rightTpPoint.position : leftTpPoint.position;
                col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}