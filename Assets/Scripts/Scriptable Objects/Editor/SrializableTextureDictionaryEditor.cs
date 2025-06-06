using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SerializableTextureDictionary))]
public class SrializableTextureDictionaryEditor : Editor
{
    private SerializableTextureDictionary _targetEditedScriptRef;

    // Gets the cast to be able to access the dictionary.
    private void OnEnable()
    {
        _targetEditedScriptRef = (SerializableTextureDictionary)target;
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
            Texture2D texture = _targetEditedScriptRef.Data[oldKey];

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical();
            int newKey = EditorGUILayout.IntField(oldKey, GUILayout.Width(30));
            if (EditorGUI.EndChangeCheck())
            {
                if (newKey != oldKey && !_targetEditedScriptRef.Data.ContainsKey(newKey))
                {
                    _targetEditedScriptRef.Data.Remove(oldKey);
                    _targetEditedScriptRef.Data[newKey] = texture;
                    allKeys[i] = newKey;
                }
                else
                {
                    if (newKey != oldKey) Debug.LogWarning("Cannot assign this key,it already exists!");
                     newKey = oldKey;
                }
            }

            bool stopper = false;

            if (GUILayout.Button("✕", GUILayout.Width(30)))
            {
                _targetEditedScriptRef.Data.Remove(newKey);
                allKeys.RemoveAt(i);
                i--;
                stopper = true;
            }
            EditorGUILayout.EndVertical();

            if (!stopper)
            {
                EditorGUI.BeginChangeCheck();
                Texture2D newTexture = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, 
                    GUILayout.Height(EditorGUIUtility.currentViewWidth-100), GUILayout.Width(EditorGUIUtility.currentViewWidth - 100));
                if (EditorGUI.EndChangeCheck())
                {
                    _targetEditedScriptRef.Data[newKey] = newTexture;
                }
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
