using UnityEngine;

[CreateAssetMenu(fileName = "Lightning", menuName = "Abilities/Lightning")]
public class LightningAbility : Ability
{
    public float damage = 15f;

    public override void Activate(GameObject caster, Transform firePoint)
    {
        Debug.Log($"{abilityName} (electricidad) lanzada por {caster.name}");
        // TODO: definir qué hace la electricidad
    }
}
