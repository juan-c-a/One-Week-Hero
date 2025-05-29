using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class WeaponButtonPrefabCreator : MonoBehaviour
{
    [Header("Settings")]
    public Font font;
    public Sprite buttonBackgroundSprite;
    public string savePath = "Assets/Prefabs/WeaponButtons/";

    [ContextMenu("Create Editable Weapon Button Prefab")]
    public void CreateEditableWeaponButtonPrefab()
    {
        GameObject buttonGO = new GameObject("WeaponButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button), typeof(VerticalLayoutGroup));
        var layout = buttonGO.GetComponent<VerticalLayoutGroup>();
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.spacing = 10;

        // Background Image
        var bgImage = buttonGO.GetComponent<Image>();
        bgImage.sprite = buttonBackgroundSprite;
        bgImage.type = Image.Type.Sliced;
        bgImage.color = Color.white;

        // Icon
        GameObject iconGO = new GameObject("Icon", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        iconGO.transform.SetParent(buttonGO.transform, false);

        // Text
        GameObject textGO = new GameObject("Weapon Name", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        textGO.transform.SetParent(buttonGO.transform, false);
        var tmp = textGO.GetComponent<TextMeshProUGUI>();
        tmp.fontSize = 24;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.enableAutoSizing = true;
        tmp.color = Color.black;

        // Add Script and wire references
        var weaponUI = buttonGO.AddComponent<WeaponButtonUI>();
        weaponUI.SetReferences(iconGO.GetComponent<Image>(), tmp);

        // Save as prefab
#if UNITY_EDITOR
        if (!AssetDatabase.IsValidFolder(savePath))
            AssetDatabase.CreateFolder("Assets/Prefabs", "WeaponButtons");

        string path = $"{savePath}WeaponButtonPrefab.prefab";
        PrefabUtility.SaveAsPrefabAsset(buttonGO, path);
        DestroyImmediate(buttonGO);
        Debug.Log($"Prefab saved to: {path}");
#endif
    }
}
