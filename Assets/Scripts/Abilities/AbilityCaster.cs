using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityCaster : MonoBehaviour
{
    [Tooltip("Habilidades equipadas: slot 0 = tecla 1, slot 1 = tecla 2, slot 2 = tecla 3")]
    public Ability[] abilities = new Ability[3];

    [Tooltip("Punto desde donde salen las habilidades (ej. la mano o la espada)")]
    public Transform firePoint;

    private float[] nextReadyTime;

    private void Awake()
    {
        nextReadyTime = new float[abilities.Length];
        if (firePoint == null) firePoint = transform;
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) TryCast(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) TryCast(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) TryCast(2);
    }

    public bool TryCast(int slot)
    {
        if (slot < 0 || slot >= abilities.Length) return false;

        Ability ability = abilities[slot];
        if (ability == null) return false;

        if (Time.time < nextReadyTime[slot])
        {
            Debug.Log($"{ability.abilityName} en cooldown");
            return false;
        }

        ability.Activate(gameObject, firePoint);
        nextReadyTime[slot] = Time.time + ability.cooldown;
        return true;
    }

    // Útil para la UI (barra de cooldown): 0 = lista, 1 = recién usada
    public float GetCooldownPercent(int slot)
    {
        Ability ability = abilities[slot];
        if (ability == null || ability.cooldown <= 0f) return 0f;
        return Mathf.Clamp01((nextReadyTime[slot] - Time.time) / ability.cooldown);
    }
}
