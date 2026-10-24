
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDefense : MonoBehaviour
{
    private PlayerMovement movement;

    public bool IsDefending { get; private set; }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        // Mantener Shift para defenderse
        bool defensePressed = Keyboard.current.zKey.isPressed;

        IsDefending = defensePressed;

        // Bloquear el movimiento mientras se defiende
        movement.MovementLocked = IsDefending;
    }
}
