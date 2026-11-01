
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackDuration = 0.25f;

    private PlayerDefense defense;
    private float attackTimer;

    public bool IsAttacking => attackTimer > 0f;

    private void Awake()
    {
        defense = GetComponent<PlayerDefense>();
    }

    private void Update()
    {
        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;

        if (Keyboard.current == null) return;

        // No se puede atacar mientras se defiende
        if (defense != null && defense.IsDefending) return;

        if (Keyboard.current.xKey.wasPressedThisFrame && !IsAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Activacion de la animación
        attackTimer = attackDuration;
        Debug.Log("Ataque hecho");
    }
}
