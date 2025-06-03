using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SerializableTextureDictionary", menuName = "Scriptable Objects/SerializableTextureDictionary"), System.Serializable]
public class SerializableTextureDictionary : AbstractSerializableDictionary<int,Texture2D, intTexture2DEntry>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<intTexture2DEntry> _savedData;

    protected override List<intTexture2DEntry> GetSavedData() => _savedData;
    protected override void SetSavedData(List<intTexture2DEntry> list) => _savedData = list;
}


/// <summary>
/// a Key and Value pair to save in the SavedData list
/// </summary>
[System.Serializable]
public struct intTexture2DEntry : AbstractEntry<int,Texture2D>
{
    [SerializeField]
    public int _key;
    [SerializeField]
    public Texture2D _value;

    public int Key { get { return _key; } set { _key = value; } }
    public Texture2D Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public void Set(int key, Texture2D value)
    {
        _key = key;
        _value = value;
    }
}
