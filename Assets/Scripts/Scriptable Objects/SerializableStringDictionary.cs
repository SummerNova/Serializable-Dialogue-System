using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SerializableStringDictionary", menuName = "Scriptable Objects/SerializableStringDictionary"), System.Serializable]
public class SerializableStringDictionary : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<Entry> SavedData;

    private Dictionary<int, string> RuntimeData = new();

    public Dictionary<int, string> Data => RuntimeData;


    /// <summary>
    /// Load the Serialized list of data from the list into the dictionary. 
    /// in other words, this is the READ of the data
    /// </summary>
    public void OnAfterDeserialize()
    {
        RuntimeData = new();
        if (SavedData == null) return;
        foreach (var entry in SavedData)
        {
            RuntimeData[entry.Key] = entry.Value;
        }
    }


    /// <summary>
    /// When unloading the SO, save the latest data from the dictionary into the saved data list. applies only in editor, as builds do not serialize.
    /// in other words, this is the WRITE of the data
    /// </summary>
    public void OnBeforeSerialize()
    {
        if (SavedData == null)
            SavedData = new List<Entry>();
        else
            SavedData.Clear();

        if (RuntimeData != null)
        {
            foreach (var KeyValuePair in RuntimeData)
            {
                Entry newEntry = new(KeyValuePair.Key, KeyValuePair.Value);
                SavedData.Add(newEntry);
            }
        }
    }
}


/// <summary>
/// a Key and Value pair to save in the SavedData list
/// </summary>
[System.Serializable]
public struct Entry
{
    public int Key;
    public string Value;

    public Entry(int key, string value)
    {
        Key = key;
        Value = value;
    }
}
