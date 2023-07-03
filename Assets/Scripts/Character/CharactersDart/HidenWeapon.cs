using System.Collections;
using System.ComponentModel;
using UnityEngine;

namespace Character.CharactersDart
{
    public class HidenWeapon : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float shootDelay;
        private bool _readyToShoot = true;
    
        public void Shoot()
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            StartCoroutine(ShootDelay());
        }
        public bool ShootPermission()
        {
            return _readyToShoot;
        }
        private IEnumerator ShootDelay()
        {
            _readyToShoot = false;
            yield return new WaitForSeconds(shootDelay);
            _readyToShoot = true;
        }
    }
}
