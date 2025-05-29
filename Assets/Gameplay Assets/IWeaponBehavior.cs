using UnityEngine;

public interface IWeaponBehavior
{
    void Initialize(Transform owner, WeaponData data);
    void Attack(Vector2 direction);
}