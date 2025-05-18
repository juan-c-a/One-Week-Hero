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
        // Aqu� llamas al m�todo real de ataque (animaci�n, da�o, etc)
    }

    private void OnUsePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Player used an object!");
        // Aqu� ejecutas la acci�n de 'usar' (abrir puerta, recoger �tem, etc)
    }
}
