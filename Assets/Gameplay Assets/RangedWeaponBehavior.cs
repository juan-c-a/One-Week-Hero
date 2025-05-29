using UnityEngine;

public class RangedWeaponBehavior : MonoBehaviour, IWeaponBehavior
{
    private WeaponData weaponData;
    private Transform owner;

    public void Initialize(Transform owner, WeaponData data)
    {
        this.owner = owner;
        this.weaponData = data;

        // Optional: Instantiate visual representation
        if (weaponData.weaponPrefab != null)
        {
            Instantiate(weaponData.weaponPrefab, owner.position, owner.rotation, owner);
        }
    }

    public void Attack(Vector2 direction)
    {
        if (weaponData.projectilePrefab == null)
        {
            Debug.LogWarning("No projectile prefab assigned to weapon: " + weaponData.weaponName);
            return;
        }

        GameObject projectile = Instantiate(
            weaponData.projectilePrefab,
            owner.position,
            Quaternion.identity
        );

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * weaponData.projectileSpeed;
        }

        Debug.Log($"[Ranged] {weaponData.weaponName} fired a projectile.");
    }
}