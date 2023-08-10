using System;
using Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Traps
{
    public class SawBlade : MonoBehaviour
    {
        [SerializeField] private float targetVelocity;
        [SerializeField] private float impulseMagnitude;
        [SerializeField] private float damage;
        [SerializeField] private Transform leftBorder;
        [SerializeField] private Transform rightBorder;
        private Transform _targetPosition;
        private Rigidbody2D _rb;
        private const float SawRunBackConstant = 0.25f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _targetPosition = leftBorder;
        }
        private void Update()
        {
            _rb.velocity = (_targetPosition.position - transform.position).normalized * targetVelocity;
            if (leftBorder.position.x + SawRunBackConstant > transform.position.x)
            {
                _targetPosition = rightBorder;
            }
            else if (rightBorder.position.x - SawRunBackConstant < transform.position.x)
            {
                _targetPosition = leftBorder;
            }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            var pushDirection = (col.transform.position - transform.position);
            col.GetComponent<Rigidbody2D>().AddForce(pushDirection * impulseMagnitude, ForceMode2D.Impulse);
            col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}