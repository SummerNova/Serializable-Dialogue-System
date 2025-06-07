using UnityEngine;
using UnityEditor;


public class SequenceValidator : EditorWindow
{

    [SerializeField] private CharacterDialogue characterDialogue;

    [MenuItem("Window/Dialogue/Validate Sequences")]
    public static void ShowWindow()
    {
        GetWindow<SequenceValidator>("Validate Sequences");
    }

    private void OnGUI()
    {
        GUILayout.Label("Character Dialogue Validator", EditorStyles.boldLabel);
        characterDialogue = (CharacterDialogue)EditorGUILayout.ObjectField("Character Dialogue", characterDialogue, typeof(CharacterDialogue), false);
        if(!characterDialogue) return;
        int elemntIndex = 1;
        foreach(Dialogue dialogue in characterDialogue.DialogueSequence)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"{elemntIndex++}: ", GUILayout.Width(20));
            EditorGUILayout.HelpBox($"{(characterDialogue.CharacterArtDict.Data[dialogue.TextureID] != null ? "✅" : "❌")}", MessageType.None);

            EditorGUILayout.HelpBox($"{(characterDialogue.Dialogues.Data[dialogue.DialogueID] != null ? "✅" : "❌")}", MessageType.None);

            EditorGUILayout.HelpBox($"{(characterDialogue.VoiceLines.Data[dialogue.VoiceLineID] != null ? "✅" : "❌")}", MessageType.None);

            EditorGUILayout.EndHorizontal();

        }
    }

    
}