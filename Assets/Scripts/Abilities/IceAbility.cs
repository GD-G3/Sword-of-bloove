using UnityEngine;

[CreateAssetMenu(fileName = "Ice", menuName = "Abilities/Ice")]
public class IceAbility : Ability
{
    public float damage = 8f;
    public float range = 4f;
    public float coneAngle = 60f;
    public LayerMask targetLayers;

    // Daño instantáneo en un cono hacia donde mira el personaje
    public override void Activate(GameObject caster, Transform firePoint)
    {
        Vector2 origin = firePoint.position;
        Vector2 facing = caster.GetComponent<PlayerMovement>().FacingDirection;

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(origin, range, targetLayers))
        {
            Vector2 toTarget = (Vector2)hit.transform.position - origin;
            if (Vector2.Angle(facing, toTarget) > coneAngle / 2f) continue;

            if (hit.TryGetComponent(out IDamageable target))
                target.TakeDamage(damage);
        }
    }
}
