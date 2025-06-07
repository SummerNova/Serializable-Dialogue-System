using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;

[Overlay(typeof(SceneView), "dialogue-creator")]
[Icon("Assets/Editor/Icons/dialogue-icon.png")] // Optional: add your own icon
public class DialogueCreatorOverlay : ToolbarOverlay
{
    DialogueCreatorOverlay() : base(DialogueCreatorButton.id) { }
}

[EditorToolbarElement(id, typeof(SceneView))]
class DialogueCreatorButton : EditorToolbarButton
{
    public const string id = "DialogueCreator/CreateDialogue";

    public DialogueCreatorButton()
    {
        text = "New Dialogue";
        tooltip = "Create a new CharacterDialogue ScriptableObject";
        icon = EditorGUIUtility.IconContent("ScriptableObject Icon").image as Texture2D;

        clicked += CreateNewDialogue;
    }

    void CreateNewDialogue()
    {
       
        CharacterDialogue newDialogue = ScriptableObject.CreateInstance<CharacterDialogue>();

      
        newDialogue.CharacterName = "New Character";
        newDialogue.DialogueSequence = new System.Collections.Generic.List<Dialogue>();

        
        newDialogue.Dialogues = new SerializableStringDictionary();
        newDialogue.VoiceLines = new SerializableAudioClipDictionary();
        newDialogue.CharacterArtDict = new SerializableTextureDictionary();

        
        newDialogue.DisplayDictByType = CharacterDialogue.DictionaryType.All;

       
        string path = EditorUtility.SaveFilePanelInProject(
            "Create New Character Dialogue",
            "NewCharacterDialogue",
            "asset",
            "Choose where to save the new Character Dialogue asset"
        );

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newDialogue, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Select the new asset in the project window
            EditorGUIUtility.PingObject(newDialogue);
            Selection.activeObject = newDialogue;

            Debug.Log($"Created new CharacterDialogue asset: {path}");
        }
    }
}