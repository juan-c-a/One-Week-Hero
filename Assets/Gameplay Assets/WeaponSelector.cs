using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private PlayerWeaponController playerWeaponController;
    [SerializeField] private InputActionReference nextWeaponAction;
    [SerializeField] private InputActionReference previousWeaponAction;

    private int currentWeaponIndex = 0;

    private void OnEnable()
    {
        nextWeaponAction.action.performed += OnNextWeapon;
        previousWeaponAction.action.performed += OnPreviousWeapon;
    }

    private void OnDisable()
    {
        nextWeaponAction.action.performed -= OnNextWeapon;
        previousWeaponAction.action.performed -= OnPreviousWeapon;
    }

    private void OnNextWeapon(InputAction.CallbackContext context)
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % playerWeaponController.equippedWeapons.Count;
        playerWeaponController.EquipWeapon(currentWeaponIndex);
    }

    private void OnPreviousWeapon(InputAction.CallbackContext context)
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + playerWeaponController.equippedWeapons.Count) % playerWeaponController.equippedWeapons.Count;
        playerWeaponController.EquipWeapon(currentWeaponIndex);
    }
}