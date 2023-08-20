using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float impulseMagnitude;
        [SerializeField] private Animator animator;
        private static readonly int IsBounce = Animator.StringToHash("IsBounce");
        public UnityEvent onBounce;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            var pushDirection = (col.transform.position - transform.position);
            var rb = col.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(pushDirection * impulseMagnitude, ForceMode2D.Impulse);
            animator.SetTrigger(IsBounce);
            onBounce.Invoke();
        }
    }
}