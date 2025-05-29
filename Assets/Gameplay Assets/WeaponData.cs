using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public bool isEnabled;
    public string weaponName;
    public Sprite weaponIcon;
    public GameObject weaponPrefab;
    public WeaponType weaponType;
    public int damage;
    public float cooldown;

    [Header("Ranged Only")]
    public GameObject projectilePrefab; // Usado solo si weaponType == Ranged
    public float projectileSpeed = 10f;
    public float projectileLifetime = 3f;
}