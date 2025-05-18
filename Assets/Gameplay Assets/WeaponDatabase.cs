using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Weapons/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public WeaponData[] allWeapons;
}
