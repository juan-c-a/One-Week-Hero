using UnityEngine;

public class MeleeWeaponBehavior : MonoBehaviour, IWeaponBehavior
{
    private WeaponData weaponData;
    private PlayerWeaponController controller;

    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask hitMask; // Define qué capas pueden recibir daño (por ejemplo: "Enemies")

    public void Initialize(PlayerWeaponController controller, WeaponData weaponData)
    {
        this.controller = controller;
        this.weaponData = weaponData;
    }

    public void Attack(Transform origin, Vector2 direction)
    {
        Debug.Log($"Melee attack with {weaponData.weaponName}");

        // Realiza una detección de colisión tipo círculo para encontrar objetivos cercanos
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin.position, attackRange, direction, 0f, hitMask);

        foreach (var hit in hits)
        {
            var damageable = hit.collider.GetComponent<DamageableBehavior>();
            if (damageable != null)
            {
                damageable.TakeDamage(weaponData.baseDamage);
                Debug.Log($"Hit {hit.collider.name} for {weaponData.baseDamage} damage");
            }
        }
    }

    public void Use()
    {
        // Aquí podrías implementar una acción secundaria si es necesario
    }

#if UNITY_EDITOR
    // Dibuja el área de ataque en modo editor para visual debugging
    private void OnDrawGizmosSelected()
    {
        if (controller != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(controller.transform.position, attackRange);
        }
    }
#endif
}
