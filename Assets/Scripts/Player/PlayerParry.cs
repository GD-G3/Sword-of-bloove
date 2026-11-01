using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// mientras se defiende (Z), si un enemigo cercano está en rojo
/// y se presiona una dirección, el jugador hace un dash y esquiva el golpe.
/// </summary>
[RequireComponent(typeof(PlayerMovement), typeof(PlayerDefense))]
public class PlayerParry : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 18f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float invulnerableTime = 0.35f;
    [SerializeField] private float cooldown = 0.8f;
    [SerializeField] private float detectRange = 2.5f;

    private PlayerMovement movement;
    private PlayerDefense defense;
    private InputAction moveAction;
    private Vector2 lastInput;
    private float invulnerableTimer;
    private float cooldownTimer;

    // Mientras sea true el jugador no recibe golpes físicos
    public bool IsParrying => invulnerableTimer > 0f;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        defense = GetComponent<PlayerDefense>();
        moveAction = InputSystem.actions?.FindAction("Player/Move");
    }

    private void Update()
    {
        if (invulnerableTimer > 0f) invulnerableTimer -= Time.deltaTime;
        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;

        Vector2 input = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        // Solo cuenta el momento en que se presiona la dirección, no mantenerla
        bool directionPressed = input.sqrMagnitude > 0.01f && lastInput.sqrMagnitude <= 0.01f;
        lastInput = input;

        if (!directionPressed || !defense.IsDefending || cooldownTimer > 0f)
            return;

        Vector2 pos = transform.position;

        if (EnemyAttack.AnyInParryWindow(pos, detectRange))
        {
            movement.Dash(input.normalized * dashSpeed, dashDuration);
            invulnerableTimer = invulnerableTime;
            cooldownTimer = cooldown;
            Debug.Log("¡Parry!");
        }
        else if (EnemyAttack.AnyAttacking(pos, detectRange))
        {
            // Muy temprano: falla y entra en cooldown
            cooldownTimer = cooldown;
            Debug.Log("Parry fallido");
        }
    }
}
