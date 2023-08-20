using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    public UnityEvent onTakeDamage;
    public float CurrentHealth { get; set; }

    private void Awake()
    {
        CurrentHealth = startHealth;
    }
    public void AddHealth(float health)
    {
        CurrentHealth += health;
        if (CurrentHealth > startHealth)
        {
            CurrentHealth = startHealth;
        }
    }
    public void GiveDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startHealth);
        onTakeDamage.Invoke();
    }
}