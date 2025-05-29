using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponData> allWeapons = new List<WeaponData>();

    private List<WeaponData> activeWeapons = new List<WeaponData>();
    private int currentWeaponIndex = 0;
    private IWeaponBehavior currentWeaponBehavior;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference nextWeaponAction;
    [SerializeField] private InputActionReference previousWeaponAction;

    private void Awake()
    {
        RefreshActiveWeapons();
    }

    private void OnEnable()
    {
        attackAction.action.performed += OnAttack;
        nextWeaponAction.action.performed += OnNextWeapon;
        previousWeaponAction.action.performed += OnPreviousWeapon;
    }

    private void OnDisable()
    {
        attackAction.action.performed -= OnAttack;
        nextWeaponAction.action.performed -= OnNextWeapon;
        previousWeaponAction.action.performed -= OnPreviousWeapon;
    }

    private void RefreshActiveWeapons()
    {
        activeWeapons = allWeapons.FindAll(w => w.isEnabled);
        EquipWeapon(currentWeaponIndex);
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        Vector2 direction = Vector2.right; // Puedes usar el input real si lo tienes
        currentWeaponBehavior?.Attack(direction);
    }

    private void OnNextWeapon(InputAction.CallbackContext ctx)
    {
        if (activeWeapons.Count == 0) return;

        currentWeaponIndex = (currentWeaponIndex + 1) % activeWeapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void OnPreviousWeapon(InputAction.CallbackContext ctx)
    {
        if (activeWeapons.Count == 0) return;

        currentWeaponIndex = (currentWeaponIndex - 1 + activeWeapons.Count) % activeWeapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void EquipWeapon(int index)
    {
        if (activeWeapons.Count == 0 || index >= activeWeapons.Count) return;

        if (currentWeaponBehavior != null)
            Destroy((MonoBehaviour)currentWeaponBehavior);

        var data = activeWeapons[index];

        switch (data.weaponType)
        {
            case WeaponType.Melee:
                currentWeaponBehavior = gameObject.AddComponent<MeleeWeaponBehavior>();
                break;
            case WeaponType.Ranged:
                currentWeaponBehavior = gameObject.AddComponent<RangedWeaponBehavior>();
                break;
        }

        currentWeaponBehavior?.Initialize(transform, data);
        Debug.Log($"Equipped: {data.weaponName}");
    }

    //  API pública
    public void DisableWeapon(WeaponData weapon)
    {
        weapon.isEnabled = false;
        RefreshActiveWeapons();
    }

    public void EnableWeapon(WeaponData weapon)
    {
        if (!allWeapons.Contains(weapon))
            allWeapons.Add(weapon);

        weapon.isEnabled = true;
        RefreshActiveWeapons();
    }
}