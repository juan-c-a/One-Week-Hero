using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] public List<WeaponData> equippedWeapons = new List<WeaponData>();
    private IWeaponBehavior currentWeaponBehavior;
    public WeaponData currentWeaponData { get; private set; }
    private int currentWeaponIndex = 0;
    private void Start()
    {
        var loadout = LoadoutManager.Instance.GetSelectedWeapons();
        if (loadout != null && loadout.Count > 0)
        {
            equippedWeapons = new List<WeaponData>(loadout);
            EquipWeapon(0);
        }
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= equippedWeapons.Count)
        {
            Debug.LogWarning("Index out of range.");
            return;
        }

        currentWeaponIndex = index;
        EquipWeapon(equippedWeapons[index]);
    }

    public void EquipWeapon(WeaponData weaponData)
    {
        currentWeaponData = weaponData;

        if (currentWeaponBehavior != null)
            Destroy((MonoBehaviour)currentWeaponBehavior);

        switch (weaponData.weaponType)
        {
            case WeaponType.Melee:
                currentWeaponBehavior = gameObject.AddComponent<MeleeWeaponBehavior>();
                break;
            case WeaponType.Ranged:
                currentWeaponBehavior = gameObject.AddComponent<RangedWeaponBehavior>();
                break;
        }

        currentWeaponBehavior.Initialize(this, weaponData);
        Debug.Log($"Equipped weapon: {weaponData.weaponName}");
    }

    public void Attack(Vector2 direction)
    {
        currentWeaponBehavior?.Attack(transform, direction);
    }
}