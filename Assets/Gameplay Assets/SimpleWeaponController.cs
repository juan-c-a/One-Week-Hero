using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleWeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();
    private int currentWeaponIndex = 0;

    [SerializeField] private Transform firePoint;       // Desde dónde dispara
    [SerializeField] private Transform weaponHolder;    // Dónde instanciar el arma visual
    [SerializeField] private float raycastLength = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector2 weaponOffsetRight = new Vector2(0.5f, 0f);
    [SerializeField] private Vector2 weaponOffsetLeft = new Vector2(-0.5f, 0f);
    private bool facingRight = true;
    private Transform currentWeaponVisual;

    private PlayerInput input;
    private GameObject currentVisualWeapon;
    private Vector2 lastAimDirection = Vector2.right;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        EquipWeapon(currentWeaponIndex);
    }

    private void OnEnable()
    {
        input.actions["Attack"].performed += OnAttack;
        input.actions["Next"].performed += OnNextWeapon;
        input.actions["Previous"].performed += OnPreviousWeapon;
        input.actions["Move"].performed += OnMove; // Para rotar el arma
    }

    private void OnDisable()
    {
        input.actions["Attack"].performed -= OnAttack;
        input.actions["Next"].performed -= OnNextWeapon;
        input.actions["Previous"].performed -= OnPreviousWeapon;
        input.actions["Move"].performed -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 moveInput = input.actions["Move"].ReadValue<Vector2>();

        // Detectar si se está moviendo
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastAimDirection = moveInput.normalized;

            // Determinar dirección horizontal
            facingRight = moveInput.x >= 0.01f;
        }

        if (currentWeaponVisual != null)
        {
            currentWeaponVisual.transform.localPosition = facingRight ? weaponOffsetRight : weaponOffsetLeft;

            Vector3 localScale = currentWeaponVisual.transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * (facingRight ? 1 : -1);
            currentWeaponVisual.transform.localScale = localScale;
        }
    }


    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if (weapons.Count == 0) return;

        WeaponData weapon = weapons[currentWeaponIndex];

        if (weapon.weaponType == WeaponType.Melee)
        {
            // Direcciones tipo abanico: centro, 45° arriba/abajo
            Vector2 dir = lastAimDirection.normalized;
            Vector2[] directions = new Vector2[]
            {
        dir,
        RotateVector(dir, 30f),
        RotateVector(dir, -30f),
        RotateVector(dir, 60f),
        RotateVector(dir, -60f)
            };

            foreach (var rayDir in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, raycastLength, enemyLayer);
                Debug.DrawRay(transform.position, rayDir * raycastLength, Color.red, 0.5f);

                if (hit.collider != null)
                {
                    var health = hit.collider.GetComponent<EnemyHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(weapon.damage);
                        Debug.Log($"Melee hit: {hit.collider.name}");
                    }
                }
            }
        }

        else if (weapon.weaponType == WeaponType.Ranged)
        {
            if (weapon.projectilePrefab == null)
            {
                Debug.LogWarning("No projectile prefab assigned.");
                return;
            }

            GameObject projectileObj = Instantiate(weapon.projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile proj = projectileObj.GetComponent<Projectile>();

            if (proj != null)
            {
                proj.Initialize(lastAimDirection, weapon.damage, weapon.projectileSpeed, 3f);
            }

            Debug.Log("Fired projectile.");
        }
    }

    private void OnNextWeapon(InputAction.CallbackContext ctx)
    {
        if (weapons.Count == 0) return;
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void OnPreviousWeapon(InputAction.CallbackContext ctx)
    {
        if (weapons.Count == 0) return;
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Count) % weapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void EquipWeapon(int index)
    {
        if (weapons.Count == 0 || index >= weapons.Count) return;

        WeaponData weapon = weapons[index];

        // Destruir el arma visual actual
        if (currentVisualWeapon != null)
        {
            Destroy(currentVisualWeapon);
        }

        // Instanciar el nuevo prefab
        if (weapon.weaponPrefab != null && weaponHolder != null)
        {
            currentVisualWeapon = Instantiate(weapon.weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        }

        Debug.Log($"Equipped weapon: {weapon.weaponName}");
    }
    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        ).normalized;
    }

}
