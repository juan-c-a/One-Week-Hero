using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private PlayerWeaponController playerWeaponController;
    [SerializeField] private Image weaponIconImage;

    private void Update()
    {
        if(playerWeaponController != null && playerWeaponController.currentWeaponData != null)
        {
            weaponIconImage.sprite = playerWeaponController.currentWeaponData.weaponIcon;
        }
    }
}