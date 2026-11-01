using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemigo de prueba con ataque físico.
/// Ciclo: espera -> preparación (amarillo) -> ventana de parry (rojo) -> golpe
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyAttack : MonoBehaviour
{
    private enum State { Idle, Windup, ParryWindow }

    [SerializeField] private PlayerHitReceiver target;
    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private float windupTime = 0.6f;
    [SerializeField] private float parryWindowTime = 0.3f;
    [SerializeField] private float hitRange = 1.5f;

    [SerializeField] private Color idleColor = Color.white;
    [SerializeField] private Color windupColor = Color.yellow;
    [SerializeField] private Color parryWindowColor = Color.red;

    private static readonly List<EnemyAttack> active = new List<EnemyAttack>();

    private SpriteRenderer sr;
    private State state = State.Idle;
    private float timer;

    public bool IsAttacking => state != State.Idle;
    public bool InParryWindow => state == State.ParryWindow;

    public static bool AnyInParryWindow(Vector2 pos, float range) =>
        active.Exists(e => e.InParryWindow && InRange(e, pos, range));

    public static bool AnyAttacking(Vector2 pos, float range) =>
        active.Exists(e => e.IsAttacking && InRange(e, pos, range));

    private static bool InRange(EnemyAttack e, Vector2 pos, float range) =>
        Vector2.Distance(e.transform.position, pos) <= range;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (target == null)
            target = FindFirstObjectByType<PlayerHitReceiver>();
        timer = attackInterval;
    }

    private void OnEnable() => active.Add(this);
    private void OnDisable() => active.Remove(this);

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;

        switch (state)
        {
            case State.Idle:
                SetState(State.Windup, windupTime, windupColor);
                break;
            case State.Windup:
                SetState(State.ParryWindow, parryWindowTime, parryWindowColor);
                break;
            case State.ParryWindow:
                Strike();
                SetState(State.Idle, attackInterval, idleColor);
                break;
        }
    }

    private void SetState(State newState, float duration, Color color)
    {
        state = newState;
        timer = duration;
        sr.color = color;
    }

    private void Strike()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > hitRange)
        {
            Debug.Log("Enemigo: ataque al aire");
            return;
        }

        HitResult result = target.ReceivePhysicalHit();
        Debug.Log($"Enemigo: golpe -> {result}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
