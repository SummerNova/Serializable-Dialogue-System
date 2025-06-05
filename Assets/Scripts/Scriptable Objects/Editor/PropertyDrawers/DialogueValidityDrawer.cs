using UnityEditor;
using UnityEngine;
using static CharacterDialogue;

[CustomPropertyDrawer(typeof(Dialogue))]
public class DialogueValidityDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Start property block
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        // Define layout: two lines
        Rect labelRow = new Rect(position.x , position.y, position.width, lineHeight);
        Rect fieldRow = new Rect(position.x , position.y + lineHeight + spacing, position.width, lineHeight);

        float width = fieldRow.width / 6;
        float space = fieldRow.width / 12;
        float xOffset = fieldRow.width /6;

        // Get properties
        SerializedProperty textureID = property.FindPropertyRelative("TextureID");
        SerializedProperty dialogueID = property.FindPropertyRelative("DialogueID");
        SerializedProperty voiceLineID = property.FindPropertyRelative("VoiceLineID");


        // Rects for each label
        Rect textureLabelRect = new Rect(labelRow.x + xOffset , labelRow.y, width, lineHeight);
        Rect dialogueLabelRect = new Rect(labelRow.x + space + width + xOffset, labelRow.y, width, lineHeight);
        Rect voiceLabelRect = new Rect(labelRow.x + 2 * (width + space) + xOffset, labelRow.y, width, lineHeight);

        // Rects for each field
        Rect textureFieldRect = new Rect(fieldRow.x + xOffset , fieldRow.y, width, lineHeight);
        Rect dialogueFieldRect = new Rect(fieldRow.x + width + space + xOffset, fieldRow.y, width, lineHeight);
        Rect voiceFieldRect = new Rect(fieldRow.x + 2 * (width + space) + xOffset, fieldRow.y, width, lineHeight);

        GUIStyle centeredLabel = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter
        };

        GUIStyle centeredField = new GUIStyle(EditorStyles.numberField)
        {
            alignment = TextAnchor.MiddleCenter
        };

        // Draw centered labels
        EditorGUI.LabelField(textureLabelRect, "Texture ID", centeredLabel);
        EditorGUI.LabelField(dialogueLabelRect, "Dialogue ID", centeredLabel);
        EditorGUI.LabelField(voiceLabelRect, "VoiceLine ID", centeredLabel);

        // Draw centered int fields manually
        textureID.intValue = EditorGUI.IntField(textureFieldRect, textureID.intValue, centeredField);
        dialogueID.intValue = EditorGUI.IntField(dialogueFieldRect, dialogueID.intValue, centeredField);
        voiceLineID.intValue = EditorGUI.IntField(voiceFieldRect, voiceLineID.intValue, centeredField);

        EditorGUI.EndProperty();
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Two lines: one for labels, one for fields
        return 2 * EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }
}
