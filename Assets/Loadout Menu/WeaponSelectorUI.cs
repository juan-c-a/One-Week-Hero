using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WeaponSelectorUI : MonoBehaviour
{
    [SerializeField] private WeaponData[] availableWeapons;
    [SerializeField] private GameObject weaponButtonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private Button confirmButton;

    private List<WeaponData> selectedWeapons = new List<WeaponData>();
    private int maxSelection = 3;

    private void Start()
    {
        GenerateWeaponButtons();
        confirmButton.onClick.AddListener(ConfirmSelection);
    }

    private void GenerateWeaponButtons()
    {
        foreach (WeaponData weapon in availableWeapons)
        {
            GameObject buttonGO = Instantiate(weaponButtonPrefab, buttonContainer);
            buttonGO.GetComponentInChildren<Text>().text = weapon.weaponName;

            Image icon = buttonGO.GetComponentInChildren<Image>();
            if (icon != null && weapon.weaponIcon != null)
            {
                icon.sprite = weapon.weaponIcon;
            }

            Button button = buttonGO.GetComponent<Button>();
            button.onClick.AddListener(() => ToggleWeaponSelection(weapon, buttonGO));
        }
    }

    private void ToggleWeaponSelection(WeaponData weapon, GameObject buttonGO)
    {
        if (selectedWeapons.Contains(weapon))
        {
            selectedWeapons.Remove(weapon);
            buttonGO.GetComponent<Image>().color = Color.white;
        }
        else
        {
            if (selectedWeapons.Count >= maxSelection)
            {
                Debug.Log("No puedes seleccionar más de 3 armas.");
                return;
            }

            selectedWeapons.Add(weapon);
            buttonGO.GetComponent<Image>().color = Color.green;
        }
    }

    private void ConfirmSelection()
    {
        if (selectedWeapons.Count == 0)
        {
            Debug.Log("Debes seleccionar al menos una arma.");
            return;
        }

        LoadoutManager.Instance.SetSelectedWeapons(selectedWeapons);
        Debug.Log("Loadout confirmado! Cargando escena...");
        SceneManager.LoadScene("GameplayScene");
    }
}