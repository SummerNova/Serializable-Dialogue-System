using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SerializableAudioClipDictionary", menuName = "Scriptable Objects/SerializableAudioClipDictionary"), System.Serializable]
public class SerializableAudioClipDictionary : AbstractSerializableDictionary<int,AudioClip, intAudioClipEntry>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<intAudioClipEntry> _savedData;

    protected override List<intAudioClipEntry> GetSavedData() => _savedData;
    protected override void SetSavedData(List<intAudioClipEntry> list) => _savedData = list;
}


/// <summary>
/// a Key and Value pair to save in the SavedData list
/// </summary>
[System.Serializable]
public struct intAudioClipEntry : AbstractEntry<int, AudioClip>
{
    [SerializeField]
    public int _key;
    [SerializeField]
    public AudioClip _value;

    public int Key { get { return _key; } set { _key = value; } }
    public AudioClip Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public void Set(int key, AudioClip value)
    {
        _key = key;
        _value = value;
    }
}
