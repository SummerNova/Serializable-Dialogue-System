using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SerializableStringDictionary", menuName = "Scriptable Objects/SerializableStringDictionary"), System.Serializable]
public class SerializableStringDictionary : AbstractSerializableDictionary<int,string,intStringEntry>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<intStringEntry> _savedData;

    protected override List<intStringEntry> GetSavedData() => _savedData;
    protected override void SetSavedData(List<intStringEntry> list) => _savedData = list;
}


/// <summary>
/// a Key and Value pair to save in the SavedData list
/// </summary>
[System.Serializable]
public struct intStringEntry : AbstractEntry<int,string>
{
    [SerializeField]
    public int _key;
    [SerializeField]
    public string _value;

    public int Key { get { return _key; } set { _key = value; } }
    public string Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public void Set(int key, string value)
    {
        _key = key;
        _value = value;
    }
}
