using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(DialogueFinder))]
public class DialogueFinderEditor : Editor
{
    public SerializedProperty CharacterName;
    public SerializedProperty CharacterDialogues;
    private DialogueFinder Ref;

    private void OnEnable()
    {
        CharacterName = serializedObject.FindProperty("CharacterName");
        CharacterDialogues = serializedObject.FindProperty("FoundDialogues");
        Ref = serializedObject.targetObject as DialogueFinder;
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Character Name", GUILayout.MinWidth(50));

        CharacterName.stringValue = EditorGUILayout.TextField(CharacterName.stringValue, GUILayout.MinWidth(100));
        
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Find All Dialogues with "+ CharacterName.stringValue))
        {
            Ref.FoundDialogues = FindAllDialoguesByCharacterName(CharacterName.stringValue);
        }

        EditorGUILayout.PropertyField(CharacterDialogues);

        EditorGUI.EndChangeCheck();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(Ref);
            AssetDatabase.SaveAssets();
        }

        serializedObject.ApplyModifiedProperties();
    }



    private List<CharacterDialogue> FindAllDialoguesByCharacterName(string Name)
    {
        List<CharacterDialogue> output = new List<CharacterDialogue>();
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (so is CharacterDialogue Dialogue && Dialogue.CharacterName.ToLower().Contains(Name.ToLower()))
            {
                output.Add(Dialogue);
            }
        }

        return output;
    }
}
