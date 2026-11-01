using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [Header("Info")]
    public string abilityName = "Nueva habilidad";
    public Sprite icon;

    [Header("Stats")]
    public float cooldown = 1f;
    public float manaCost = 0f;

    // Cada habilidad define aquí lo que hace.
    // caster = quien la lanza, firePoint = desde dónde sale.
    public abstract void Activate(GameObject caster, Transform firePoint);
}
