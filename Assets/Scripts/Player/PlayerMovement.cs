using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Movimiento top-down en 8 direcciones
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 30f;
    public bool MovementLocked { get; set; }

    private Rigidbody2D rb;
    private InputAction moveAction;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        moveAction = InputSystem.actions?.FindAction("Player/Move");
    }

    private void OnEnable() => moveAction?.Enable();

    private void Update()
    {
        moveInput = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        if (MovementLocked){
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 targetVelocity = moveInput * moveSpeed;
        float rate = moveInput.sqrMagnitude > 0.01f ? acceleration : deceleration;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, rate * Time.fixedDeltaTime);
    }
}
