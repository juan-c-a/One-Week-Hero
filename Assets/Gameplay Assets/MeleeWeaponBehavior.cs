using UnityEngine;

public class MeleeWeaponBehavior : MonoBehaviour, IWeaponBehavior
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
        // Implement melee attack logic, e.g., raycast or animation trigger
        Debug.Log($"[Melee] {weaponData.weaponName} attacks in direction {direction}");

        // Example: detect hit in front of player
        RaycastHit2D hit = Physics2D.Raycast(owner.position, direction, 1.5f);
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            // Apply damage or effects here
        }
    }
}