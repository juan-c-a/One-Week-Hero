using UnityEngine;
using System.Collections.Generic;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance { get; private set; }

    private List<WeaponData> selectedWeapons = new List<WeaponData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedWeapons(List<WeaponData> weapons)
    {
        selectedWeapons = new List<WeaponData>(weapons);
    }

    public List<WeaponData> GetSelectedWeapons()
    {
        return selectedWeapons;
    }
}
