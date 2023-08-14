using Unity.VisualScripting;
using UnityEngine;
public class PowerFullWindEffect : MonoBehaviour
{
    [SerializeField] private float powerOfWind = 15f;
    private Rigidbody2D _rb;
    private bool _isTriggered;
    private void FixedUpdate()
    {
        if (!_isTriggered) return;
        _rb.AddForce(new Vector2(-powerOfWind,0f));
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _rb = col.gameObject.GetComponent<Rigidbody2D>();
            _isTriggered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isTriggered = false;
        }
    }
}