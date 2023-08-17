using System;
using Character;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyScripts.BehaviorLogic.Attack
{
    public class EnemyMagicBall : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private float speed = 20f;
        [SerializeField] private float damage = 40f;
        [SerializeField] private float timer = 5f;
        [SerializeField] private float sharedImpulseMagnitude = 3f;
        private float _timeSinceInstance;
        private CharacterController2D _opponent;
        void Start()
        {
            _opponent = FindFirstObjectByType<CharacterController2D>();
            rb.velocity = (_opponent.transform.position-transform.position).normalized * speed; // после появления префаба снаряда задаем ему скорость полета
        }
        private void Update()
        {
            _timeSinceInstance += Time.deltaTime;
            if (_timeSinceInstance > timer)
            {
                OnEndOfLifeCycle();
            }
        }
        private void OnTriggerEnter2D(Collider2D hitInfo) //проверяем куда попал снаряд, если во врага, то снимаем хп
        {
            if (!hitInfo.CompareTag("Player")) return;
            hitInfo.GetComponent<Health>().TakeDamage(damage);
            hitInfo.GetComponent<Rigidbody2D>().AddForce(rb.velocity * sharedImpulseMagnitude, ForceMode2D.Impulse);
            OnEndOfLifeCycle();
        }
        private void OnEndOfLifeCycle()
        {
            Instantiate(impactEffect, transform.position, transform.rotation); //создаем эффект попадания снаряда
            Destroy(gameObject);//дестроим снаряд после попадания
        }
    }
}