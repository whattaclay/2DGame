using UnityEngine;

namespace Environment
{
    public class HealthCrystalEffect : MonoBehaviour
    {
        private float _healthToAdd;
        private Health _bodyToFollow;
        private void Update()
        {
            transform.position = _bodyToFollow.transform.position;
        }
        public void SetBodyToFollow(Health body, float healthToAdd)
        {
            _bodyToFollow = body;
            _healthToAdd = healthToAdd;
        }
        public void HealthUp()
        {
            _bodyToFollow.GetComponent<Health>().AddHealth(_healthToAdd);
        }
    }
}