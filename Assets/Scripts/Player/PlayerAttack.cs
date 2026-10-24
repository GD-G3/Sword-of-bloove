
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Activacion de la animación
        Debug.Log("Ataque hecho");
    }
}
