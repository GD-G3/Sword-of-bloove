using UnityEngine;

/// <summary>
/// Mantiene al jugador dentro de un rectángulo centrado en este objeto
/// </summary>
public class MapBounds : MonoBehaviour
{
    [SerializeField] private Vector2 size = new Vector2(20f, 12f);
    [SerializeField] private Rigidbody2D target;

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 min = (Vector2)transform.position - size / 2f;
        Vector2 max = (Vector2)transform.position + size / 2f;

        Vector2 pos = target.position;
        Vector2 vel = target.linearVelocity;

        // Anular la velocidad que empuja hacia afuera del borde
        if (pos.x <= min.x && vel.x < 0f || pos.x >= max.x && vel.x > 0f) vel.x = 0f;
        if (pos.y <= min.y && vel.y < 0f || pos.y >= max.y && vel.y > 0f) vel.y = 0f;

        target.position = new Vector2(Mathf.Clamp(pos.x, min.x, max.x), Mathf.Clamp(pos.y, min.y, max.y));
        target.linearVelocity = vel;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
