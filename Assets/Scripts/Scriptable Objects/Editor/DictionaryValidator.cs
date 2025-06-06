

using System;
using UnityEditor;
using UnityEngine;

public static class DictionaryValidator {


    //<summary>
    /// Validates all Serializable Dictionaries in the project using the IVlidateable .
    /// </summary>
    
    [MenuItem("Tools/Dictionaries/Validate All Serializable Dictionaries %#J")]
    public static bool ValidateAll(){
        bool allValid = true;
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            Type assettype = asset.GetType();
            if (assettype.BaseType.IsGenericType && assettype.BaseType.GetGenericTypeDefinition() == typeof(AbstractSerializableDictionary<,,>))
            {
                if(asset is IValidateable validateable){
                    if (!validateable.IsValid())
                    {
                        if(allValid) allValid = false;
                    }
                }
                
                
            }
        }
        if (allValid)
        {
            Debug.Log("All Serializable Dictionaries are valid.");
            return true;
        }
        else
        {
            Debug.LogWarning("Some Serializable Dictionaries are invalid. Check the console for details.");
            return false;
        }
    }
}