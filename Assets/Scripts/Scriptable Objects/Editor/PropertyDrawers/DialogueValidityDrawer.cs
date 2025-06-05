using UnityEditor;
using UnityEngine;
using static CharacterDialogue;

[CustomPropertyDrawer(typeof(AbstractEntry<int,string>))]
public class DialogueValidityDrawer: PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

    }
}
