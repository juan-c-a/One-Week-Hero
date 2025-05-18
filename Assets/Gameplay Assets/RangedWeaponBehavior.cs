using UnityEngine;

public class RangedWeaponBehavior : MonoBehaviour, IWeaponBehavior
{
    private WeaponData weaponData;
    private PlayerWeaponController controller;

    public void Initialize(PlayerWeaponController controller, WeaponData weaponData)
    {
        this.controller = controller;
        this.weaponData = weaponData;
    }

    public void Attack(Transform origin, Vector2 direction)
    {
        Debug.Log($"Ranged attack with {weaponData.weaponName}");
        if (weaponData.projectilePrefab != null)
        {
            GameObject proj = Instantiate(weaponData.projectilePrefab, origin.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if(rb != null)
                rb.linearVelocity = direction.normalized * 10f;
        }
    }

    public void Use()
    {
        // Otra acción
    }
}