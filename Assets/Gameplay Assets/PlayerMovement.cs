using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerPro : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference move;

    private Rigidbody2D playerRigidbody;
    private Vector2 movementInput;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        move.action.performed += OnMovePerformed;
        move.action.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        move.action.performed -= OnMovePerformed;
        move.action.canceled -= OnMoveCanceled;
        move.action.Disable();
    }

    private void FixedUpdate()
    {
        playerRigidbody.linearVelocity = movementInput * moveSpeed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
}
