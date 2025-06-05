using UnityEditor;
using UnityEngine;
using static CharacterDialogue;

[CustomPropertyDrawer(typeof(DictionaryType))]
public class DictionaryTypeDrawer:PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        DictionaryType type = (DictionaryType)property.enumValueIndex;
        Color defaultColor = GUI.color;
        GUI.color = GetTypeColor(type);
        property.enumValueIndex = EditorGUI.Popup(position,
            label.text, property.enumValueIndex, property.enumDisplayNames);
        GUI.color = defaultColor;
        EditorGUI.EndProperty();
    }

    private Color GetTypeColor(DictionaryType Type)
    {
        switch (Type)
        {
            case DictionaryType.All:
                return Color.white; 
            case DictionaryType.AudioClip:
                return Color.green; 
            case DictionaryType.Text:
                return Color.cyan;
            case DictionaryType.Texture:
                return Color.yellow;
            default:
                return Color.gray; 
        }
    }

}
