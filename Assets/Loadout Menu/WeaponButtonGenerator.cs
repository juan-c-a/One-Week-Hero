using System.Collections.Generic;
using UnityEngine;

public class WeaponButtonGenerator : MonoBehaviour
{
    public WeaponButtonFactory factory;
    public Transform buttonParent;
    public List<WeaponData> weapons;

    public void GenerateButtons()
    {
        foreach (Transform child in buttonParent)
            DestroyImmediate(child.gameObject);

        foreach (var weapon in weapons)
        {
            factory.CreateWeaponButton(weapon);
        }
    }
}
