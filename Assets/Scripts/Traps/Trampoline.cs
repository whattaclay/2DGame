using System;
using UnityEngine;

namespace Traps
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float impulseMagnitude;
        [SerializeField] private Animator animator;
        private static readonly int IsBounce = Animator.StringToHash("IsBounce");

        private void OnTriggerEnter2D(Collider2D col)
        {
            var pushDirection = (col.transform.position - transform.position);
            col.GetComponent<Rigidbody2D>().AddForce(pushDirection * impulseMagnitude, ForceMode2D.Impulse);
            animator.SetTrigger(IsBounce);
        }
    }
}