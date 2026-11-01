using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "Abilities/Fire")]
public class FireAbility : Ability
{
    public float damage = 10f;
    public float radius = 2.5f;
    public LayerMask targetLayers;

    // Explosión instantánea en un área alrededor del personaje
    public override void Activate(GameObject caster, Transform firePoint)
    {
        Vector2 center = caster.transform.position;

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(center, radius, targetLayers))
        {
            if (hit.TryGetComponent(out IDamageable target))
                target.TakeDamage(damage);
        }
    }
}
