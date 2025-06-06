using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractSerializableDictionary<TKey,TValue,TEntry> : ScriptableObject, ISerializationCallbackReceiver, IValidateable
    where TEntry : AbstractEntry<TKey, TValue>, new()
{
    protected List<TEntry> SavedData
    {
        get => GetSavedData(); // implemented in concrete class
        set => SetSavedData(value);
    }

    protected abstract List<TEntry> GetSavedData();
    protected abstract void SetSavedData(List<TEntry> list);

    protected Dictionary<TKey, TValue> RuntimeData = new();

    public Dictionary<TKey, TValue> Data => RuntimeData;


    /// <summary>
    /// Load the Serialized list of data from the list into the dictionary. 
    /// in other words, this is the READ of the data
    /// </summary>
    public void OnAfterDeserialize()
    {
        RuntimeData = new();
        if (SavedData == null) return;
        foreach (var abstractEntry in SavedData)
        {
            RuntimeData[abstractEntry.Key] = abstractEntry.Value;
        }
    }


    /// <summary>
    /// When unloading the SO, save the latest data from the dictionary into the saved data list. applies only in editor, as builds do not serialize.
    /// in other words, this is the WRITE of the data
    /// </summary>
    public void OnBeforeSerialize()
    {
        if (SavedData == null)
            SavedData = new List<TEntry>();
        else
            SavedData.Clear();

        if (RuntimeData != null)
        {
            foreach (var KeyValuePair in RuntimeData)
            {
                TEntry newAbstractEntry = new();
                newAbstractEntry.Set(KeyValuePair.Key, KeyValuePair.Value);
                SavedData.Add(newAbstractEntry);
            }
        }
    }

    public virtual bool IsValid()
    {
        return true;
    }
}


/// <summary>
/// a Key and Value pair to save in the SavedData list
/// </summary>
public interface AbstractEntry<TKey,TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public void Set(TKey key, TValue value);
    
}
