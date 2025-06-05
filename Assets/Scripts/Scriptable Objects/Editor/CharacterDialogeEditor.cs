using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

[CustomEditor(typeof(CharacterDialogue))]
public class CharacterDialogueEditor : Editor
{
    public SerializedProperty DialogueTextData;
    public SerializedProperty DialogueAudioData;
    public SerializedProperty DialogeTexture2DData;
    private CharacterDialogue _targetRef;
    private void OnEnable()
    {
        _targetRef = (CharacterDialogue)target;
        DialogueTextData = serializedObject.FindProperty("Dialogues");
        DialogeTexture2DData = serializedObject.FindProperty("CharacterArtDict");
        DialogueAudioData = serializedObject.FindProperty("VoiceLines");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();

        switch (_targetRef.DisplayDictByType)
        {
            case CharacterDialogue.DictionaryType.Texture:
                DisplayTextureDictionary();
                break;
            case CharacterDialogue.DictionaryType.Text:
                DisplayTextDictionary();
                break;
            case CharacterDialogue.DictionaryType.AudioClip:
                DisplayAudioDictionary();
                break;
            case CharacterDialogue.DictionaryType.All:
                DisplayTextDictionary();
                DisplayAudioDictionary();
                DisplayTextureDictionary();
                break;

        }

        

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_targetRef);
            AssetDatabase.SaveAssets();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayTextureDictionary()
    {
        if (DialogueAudioData.objectReferenceValue != null)
        {
            EditorGUILayout.LabelField("Texture Contents", EditorStyles.boldLabel);


            List<int> allKeys = new List<int>(_targetRef.CharacterArtDict.Data.Keys);


            for (int i = 0; i < allKeys.Count; i++)
            {
                int oldKey = allKeys[i];
                Texture2D oldValue = _targetRef.CharacterArtDict.Data[oldKey];

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUI.BeginChangeCheck();
              
                int newKey = EditorGUILayout.IntField(oldKey, GUILayout.Width(30));

                if (EditorGUI.EndChangeCheck())
                {
                    if (newKey != oldKey && !_targetRef.CharacterArtDict.Data.ContainsKey(newKey))
                    {
                        _targetRef.CharacterArtDict.Data.Remove(oldKey);
                        _targetRef.CharacterArtDict.Data[newKey] = oldValue;
                        allKeys[i] = newKey;
                    }
                    else
                    {
                        if(newKey != oldKey) Debug.LogWarning("Cannot assign this key,it already exists!");

                        newKey = oldKey;
                    } 
                }

                bool stopper = false;

                if (GUILayout.Button("✕", GUILayout.Width(20)))
                {
                    _targetRef.CharacterArtDict.Data.Remove(newKey);
                    allKeys.RemoveAt(i);
                    stopper = true;
                    i--;
                }

                EditorGUILayout.EndVertical();

                if (!stopper)
                {

                    EditorGUI.BeginChangeCheck();
                    Texture2D newValue = (Texture2D)EditorGUILayout.ObjectField(oldValue, typeof(Texture2D), false,
                        GUILayout.Height(EditorGUIUtility.currentViewWidth - 100), GUILayout.Width(EditorGUIUtility.currentViewWidth - 100));

                    if (EditorGUI.EndChangeCheck())
                    {
                        _targetRef.CharacterArtDict.Data[newKey] = newValue;
                    }

                }
                

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Add New Entry"))
            {
                int defaultKey = 0;
                while (_targetRef.CharacterArtDict.Data.ContainsKey(defaultKey))
                {
                    defaultKey++;
                }

                _targetRef.CharacterArtDict.Data[defaultKey] = null;
            }
        }
    }


    private void DisplayAudioDictionary()
    {
        if (DialogueAudioData.objectReferenceValue != null)
        {
            EditorGUILayout.LabelField("Voice Line Contents", EditorStyles.boldLabel);


            List<int> allKeys = new List<int>(_targetRef.VoiceLines.Data.Keys);


            for (int i = 0; i < allKeys.Count; i++)
            {
                int oldKey = allKeys[i];
                AudioClip oldValue = _targetRef.VoiceLines.Data[oldKey];

                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                int newKey = EditorGUILayout.IntField(oldKey, GUILayout.Width(30));
                if (EditorGUI.EndChangeCheck())
                {
                    if (newKey != oldKey && !_targetRef.VoiceLines.Data.ContainsKey(newKey))
                    {
                        _targetRef.VoiceLines.Data.Remove(oldKey);
                        _targetRef.VoiceLines.Data[newKey] = oldValue;
                        allKeys[i] = newKey;
                    }
                    else
                    {
                        if (newKey != oldKey) Debug.LogWarning("Cannot assign this key,it already exists!");
                        newKey = oldKey;
                    }
                    
                }

                EditorGUI.BeginChangeCheck();
                AudioClip newValue = (AudioClip)EditorGUILayout.ObjectField(oldValue,typeof(AudioClip) ,GUILayout.MinWidth(100));
                if (EditorGUI.EndChangeCheck())
                {
                    _targetRef.VoiceLines.Data[newKey] = newValue;
                }


                if (GUILayout.Button("✕", GUILayout.Width(20)))
                {
                    _targetRef.VoiceLines.Data.Remove(newKey);
                    allKeys.RemoveAt(i);
                    i--;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Add New Entry"))
            {
                int defaultKey = 0;
                while (_targetRef.VoiceLines.Data.ContainsKey(defaultKey))
                {
                    defaultKey++;
                }

                _targetRef.VoiceLines.Data[defaultKey] = null;
            }
        }
    }

    private void DisplayTextDictionary()
    {
        if (DialogueTextData.objectReferenceValue != null)
        {
            EditorGUILayout.LabelField("Dialogues Contents", EditorStyles.boldLabel);


            List<int> allKeys = new List<int>(_targetRef.Dialogues.Data.Keys);


            for (int i = 0; i < allKeys.Count; i++)
            {
                int oldKey = allKeys[i];
                string oldValue = _targetRef.Dialogues.Data[oldKey];

                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                int newKey = EditorGUILayout.IntField(oldKey, GUILayout.Width(30));
                if (EditorGUI.EndChangeCheck())
                {
                    if (newKey != oldKey && !_targetRef.Dialogues.Data.ContainsKey(newKey))
                    {
                        _targetRef.Dialogues.Data.Remove(oldKey);
                        _targetRef.Dialogues.Data[newKey] = oldValue;
                        allKeys[i] = newKey;
                    }
                    else
                    {
                        if (newKey != oldKey) Debug.LogWarning("Cannot assign this key,it already exists!");
                        newKey = oldKey;
                    }
                }

                EditorGUI.BeginChangeCheck();
                string newValue = EditorGUILayout.TextField(oldValue, GUILayout.MinWidth(100));
                if (EditorGUI.EndChangeCheck())
                {
                    _targetRef.Dialogues.Data[newKey] = newValue;
                }


                if (GUILayout.Button("✕", GUILayout.Width(20)))
                {
                    _targetRef.Dialogues.Data.Remove(newKey);
                    allKeys.RemoveAt(i);
                    i--;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Add New Entry"))
            {
                int defaultKey = allKeys.Count;
                int suffix = 0;
                while (_targetRef.Dialogues.Data.ContainsKey(defaultKey + suffix))
                {
                    suffix++;
                }

                _targetRef.Dialogues.Data[defaultKey + suffix] = $"{defaultKey}";
            }
        }
    }
}
