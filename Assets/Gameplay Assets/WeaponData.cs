using UnityEngine;

public enum WeaponType { Melee, Ranged }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;
    public Sprite weaponIcon;
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;  // Solo para armas a distancia
    public int baseDamage;
    public float attackSpeed;
}