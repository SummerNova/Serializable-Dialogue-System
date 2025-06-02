using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CharacterDialogue))]
public class CharacterDialogueEditor : Editor
{
    public SerializedProperty DialogueTextData;
    private CharacterDialogue _targetRef;
    private int latest = 0;

    private void OnEnable()
    {
        _targetRef = (CharacterDialogue)target;
        DialogueTextData = serializedObject.FindProperty("Dialogues");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(DialogueTextData);
        serializedObject.ApplyModifiedProperties();

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
                    else newKey = oldKey;
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
                latest = defaultKey + suffix;
            }
        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(_targetRef);
            AssetDatabase.SaveAssets();
            //Debug.Log(_targetRef.Dialogues[latest]);
        }

        serializedObject.ApplyModifiedProperties();
    }


}
