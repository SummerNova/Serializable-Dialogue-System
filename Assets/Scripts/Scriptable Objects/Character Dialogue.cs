using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Scriptable object that contains the references to a specific dialogue text, audio, and character art, and the dialogue sequence.
/// 
/// each dialogue contains the ID references for each line, voiceline audioclip and character art Texture2D to attach to the dialogue
/// </summary>
[CreateAssetMenu(fileName = "CharacterDialogue", menuName = "Scriptable Objects/CharacterDialogue")]
public class CharacterDialogue : ScriptableObject
{
    public string CharacterName;
    public List<Dialogue> DialogueSequence;


    public SerializableTextureDictionary CharacterArtDict;
    public SerializableStringDictionary Dialogues;
    public SerializableAudioClipDictionary VoiceLines;

    [SerializeField] public DictionaryType DisplayDictByType;


    public enum DictionaryType
    { All, Texture, Text, AudioClip }

}


/// <summary>
/// a dialogue contains the IDs of its text, voiceline, and attached texture
/// </summary>
[System.Serializable]
public struct Dialogue 
{
    public int TextureID;
    public int DialogueID;
    public int VoiceLineID;
}

