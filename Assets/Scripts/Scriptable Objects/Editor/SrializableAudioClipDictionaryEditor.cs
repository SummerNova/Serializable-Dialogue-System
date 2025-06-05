using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SerializableAudioClipDictionary))]
public class SrializableAudioClipDictionaryEditor: Editor
{
    private SerializableAudioClipDictionary _targetEditedScriptRef;

    // Gets the cast to be able to access the dictionary.
    private void OnEnable()
    {
        _targetEditedScriptRef = (SerializableAudioClipDictionary)target;
    }

    public override void OnInspectorGUI()
    {
        // to pull data from the object into the editor 
        serializedObject.Update();

        EditorGUILayout.LabelField("Dictionary Data", EditorStyles.boldLabel);


        List<int> allKeys = new List<int>(_targetEditedScriptRef.Data.Keys);


        for (int i = 0; i < allKeys.Count; i++)
        {
            int oldKey = allKeys[i];
            AudioClip oldValue = _targetEditedScriptRef.Data[oldKey];

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            int newKey = EditorGUILayout.IntField(oldKey, GUILayout.Width(30));
            if (EditorGUI.EndChangeCheck())
            {
                if (newKey != oldKey && !_targetEditedScriptRef.Data.ContainsKey(newKey))
                {
                    _targetEditedScriptRef.Data.Remove(oldKey);
                    _targetEditedScriptRef.Data[newKey] = oldValue;
                    allKeys[i] = newKey;
                }
                else newKey = oldKey;
            }

            EditorGUI.BeginChangeCheck();
            AudioClip newValue = (AudioClip)EditorGUILayout.ObjectField(oldValue, typeof(AudioClip),GUILayout.MinWidth(100));
            if (EditorGUI.EndChangeCheck())
            {
                _targetEditedScriptRef.Data[newKey] = newValue;
            }


            if (GUILayout.Button("✕", GUILayout.Width(20)))
            {
                _targetEditedScriptRef.Data.Remove(newKey);
                allKeys.RemoveAt(i);
                i--;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add New Entry"))
        {
            int defaultKey = 0;
            while (_targetEditedScriptRef.Data.ContainsKey(defaultKey))
            {
                defaultKey++;
            }

            _targetEditedScriptRef.Data[defaultKey] = null;
        }



        if (GUI.changed)
        {
            EditorUtility.SetDirty(_targetEditedScriptRef);
            AssetDatabase.SaveAssets();
        }

        // to push data from the editor into the object 
        serializedObject.ApplyModifiedProperties();
    }
}
