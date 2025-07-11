using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerPro : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference move;
    [SerializeField] private Animator animator;
    private PlayerHealth playerHealth;
    private Rigidbody2D playerRigidbody;
    private Vector2 movementInput;

    private void Update()
    {
        animator.SetFloat("XSpeed", movementInput.x);
        animator.SetFloat("YSpeed", movementInput.y);
    }
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
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
        if (playerHealth.isDead != true)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
}
