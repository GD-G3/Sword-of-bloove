using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 30f;

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0f);
        Debug.Log($"{name} recibió {amount} de daño ({CurrentHealth}/{maxHealth})");

        if (CurrentHealth <= 0f)
            Destroy(gameObject);
    }
}
