using EnemyScripts;
using EnemyScripts.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Attacks.DistanceAttack
{
    public class MagicBall : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private float speed = 20f;
        [SerializeField] private float damage = 40f;

        void Start()
        {
            rb.velocity = transform.right * speed; // после появления префаба снаряда задаем ему скорость полета
        }

        private void OnTriggerEnter2D(Collider2D hitInfo) //проверяем куда попал снаряд, если во врага, то снимаем хп
        {
            if (!hitInfo.CompareTag("Enemy") && !hitInfo.CompareTag("Environment")) return;
            var enemy = hitInfo.GetComponent<Enemy>();
            if (enemy!= null)
            {
                enemy.Damage(damage);
            }
            Instantiate(impactEffect, transform.position, transform.rotation); //создаем эффект попадания снаряда
            Destroy(gameObject); //дестроим снаряд после попадания
        }
    }
}
