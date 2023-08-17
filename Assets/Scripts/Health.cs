using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
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
    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startHealth);
    }
}