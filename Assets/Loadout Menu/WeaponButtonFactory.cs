using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponButtonFactory : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform parentTransform;

    [Header("Styling")]
    [SerializeField] private Sprite buttonBackgroundSprite;
    [SerializeField] private Vector2 buttonSize = new Vector2(200, 250);
    [SerializeField] private Vector2 iconSize = new Vector2(100, 100);
    [SerializeField] private int fontSize = 28;
    [SerializeField] private Color fontColor = Color.black;
    [SerializeField] private Color backgroundColor = Color.white;

    public GameObject CreateWeaponButton(WeaponData weaponData)
    {
        // Botón principal
        GameObject buttonGO = new GameObject("WeaponButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        buttonGO.transform.SetParent(parentTransform, false);
        RectTransform buttonRect = buttonGO.GetComponent<RectTransform>();
        buttonRect.sizeDelta = buttonSize;

        // Imagen de fondo
        Image background = buttonGO.GetComponent<Image>();
        background.sprite = buttonBackgroundSprite;
        background.type = Image.Type.Sliced;
        background.color = backgroundColor;

        // CONTENEDOR manual (para layout vertical)
        GameObject layoutGO = new GameObject("Content", typeof(RectTransform));
        layoutGO.transform.SetParent(buttonGO.transform, false);
        RectTransform layoutRect = layoutGO.GetComponent<RectTransform>();
        layoutRect.anchorMin = Vector2.zero;
        layoutRect.anchorMax = Vector2.one;
        layoutRect.offsetMin = Vector2.zero;
        layoutRect.offsetMax = Vector2.zero;

        // === ICONO ===
        GameObject iconGO = new GameObject("Icon", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        iconGO.transform.SetParent(layoutGO.transform, false);
        RectTransform iconRect = iconGO.GetComponent<RectTransform>();
        iconRect.sizeDelta = iconSize;
        iconRect.anchoredPosition = new Vector2(0, 40); // Ajustable
        iconRect.anchorMin = new Vector2(0.5f, 1f);
        iconRect.anchorMax = new Vector2(0.5f, 1f);
        iconRect.pivot = new Vector2(0.5f, 1f);
        Image iconImage = iconGO.GetComponent<Image>();
        iconImage.preserveAspect = true;

        // === TEXTO ===
        GameObject textGO = new GameObject("Weapon Name", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        textGO.transform.SetParent(layoutGO.transform, false);
        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(buttonSize.x - 20, 40);
        textRect.anchoredPosition = new Vector2(0, -60); // Ajustable
        textRect.anchorMin = new Vector2(0.5f, 1f);
        textRect.anchorMax = new Vector2(0.5f, 1f);
        textRect.pivot = new Vector2(0.5f, 1f);

        var tmp = textGO.GetComponent<TextMeshProUGUI>();
        tmp.fontSize = fontSize;
        tmp.color = fontColor;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.enableAutoSizing = true;

        // Enlazar script personalizado
        var weaponUI = buttonGO.AddComponent<WeaponButtonUI>();
        weaponUI.SetReferences(iconImage, tmp);
        weaponUI.Setup(weaponData);

        return buttonGO;
    }
}
