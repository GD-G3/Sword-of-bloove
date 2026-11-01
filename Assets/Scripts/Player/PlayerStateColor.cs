using UnityEngine;

/// <summary>
/// Cambia el color del sprite según el estado: parry, ataque o defensa
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerStateColor : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color attackColor = new Color(1f, 0.55f, 0f);
    [SerializeField] private Color defenseColor = new Color(0.25f, 0.5f, 1f);
    [SerializeField] private Color parryColor = Color.green;

    private SpriteRenderer sr;
    private PlayerAttack attack;
    private PlayerDefense defense;
    private PlayerParry parry;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        attack = GetComponent<PlayerAttack>();
        defense = GetComponent<PlayerDefense>();
        parry = GetComponent<PlayerParry>();
    }

    private void LateUpdate()
    {
        // Prioridad: parry > ataque > defensa > normal
        if (parry != null && parry.IsParrying)
            sr.color = parryColor;
        else if (attack != null && attack.IsAttacking)
            sr.color = attackColor;
        else if (defense != null && defense.IsDefending)
            sr.color = defenseColor;
        else
            sr.color = normalColor;
    }
}
