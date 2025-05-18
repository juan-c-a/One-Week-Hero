using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference useAction;

    private void OnEnable()
    {
        attackAction.action.performed += OnAttackPerformed;
        useAction.action.performed += OnUsePerformed;
    }

    private void OnDisable()
    {
        attackAction.action.performed -= OnAttackPerformed;
        useAction.action.performed -= OnUsePerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Player is Attacking!");
        // Aquí llamas al método real de ataque (animación, daño, etc)
    }

    private void OnUsePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Player used an object!");
        // Aquí ejecutas la acción de 'usar' (abrir puerta, recoger ítem, etc)
    }
}
