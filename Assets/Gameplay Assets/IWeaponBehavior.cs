using UnityEngine;

public interface IWeaponBehavior
{
    void Initialize(PlayerWeaponController controller, WeaponData weaponData);
    void Attack(Transform origin, Vector2 direction);
    void Use();
}