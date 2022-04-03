using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextAreaString)),CanEditMultipleObjects]
public class TextAreaStringEditorScript : Editor
{
    public SerializedProperty textAreaStringElement;

    private void OnEnable()
    {
        this.textAreaStringElement = serializedObject.FindProperty("targetString");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        this.textAreaStringElement.stringValue = EditorGUILayout.TextArea(this.textAreaStringElement.stringValue, GUILayout.MaxHeight(70));
        serializedObject.ApplyModifiedProperties();
    }
}
