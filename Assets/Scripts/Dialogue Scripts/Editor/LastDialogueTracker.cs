using UnityEditor;
using UnityEngine;



public static class LastSelectionTracker
{
    private const string PrefKey = "LastSelectedDialogue";

    public static CharacterDialogue LastSelected
    {
        get
        {
            string path = EditorPrefs.GetString(PrefKey, null);
            return !string.IsNullOrEmpty(path) ? AssetDatabase.LoadAssetAtPath<CharacterDialogue>(path) : null;
        }
        set
        {
            if (AssetDatabase.Contains(value))
            {
                string path = AssetDatabase.GetAssetPath(value);
                EditorPrefs.SetString(PrefKey, path);
            }
        }
    }

    static LastSelectionTracker()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    private static void OnSelectionChanged()
    {
        if (Selection.activeObject != null && Selection.activeObject is CharacterDialogue lastDialogue)
        {
            LastSelected = lastDialogue;
        }
    }

    [MenuItem("Tools/Dialogues/Jump to Last Selected Dialogue %#Y")]
    static void JumpToLastSelected()
    {
        if (LastSelected != null)
        {
            EditorGUIUtility.PingObject(LastSelected);
            Selection.activeObject = LastSelected;
        }
        else{
            string[] guids = AssetDatabase.FindAssets("t:CharacterDialogue");
            if (guids.Length > 0)
            {
                Debug.Log("<b>Dialogue Tracker</b> \n No last selected dialogue found. Moving to the first dialogue found");
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                CharacterDialogue firstDialogue = AssetDatabase.LoadAssetAtPath<CharacterDialogue>(path);
                EditorGUIUtility.PingObject(firstDialogue);
                Selection.activeObject = firstDialogue;
            }
            else
            {
                Debug.LogWarning("No CharacterDialogues found in the project.");
            }
        }
    }
}