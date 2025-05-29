using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponButtonGenerator))]
public class WeaponButtonGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WeaponButtonGenerator generator = (WeaponButtonGenerator)target;

        if (GUILayout.Button("Generate Weapon Buttons"))
        {
            generator.GenerateButtons();
        }
    }
}
