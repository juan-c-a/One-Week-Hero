using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WeaponButtonUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;

    private WeaponData weaponData;

    public void Setup(WeaponData data)
    {
        weaponData = data;

        Debug.Log("Setting up weapon: " + data.weaponName);

        if (iconImage != null)
            iconImage.sprite = data.weaponIcon;

        if (nameText != null)
            nameText.text = data.weaponName;
        else
            Debug.LogWarning("nameText is null!");
    }


    public void SetReferences(Image icon, TextMeshProUGUI nameLabel)
    {
        iconImage = icon;
        nameText = nameLabel;
    }
}
