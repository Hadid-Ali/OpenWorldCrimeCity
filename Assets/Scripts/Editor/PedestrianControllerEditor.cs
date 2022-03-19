using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterController))]
public class PedestrianControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharacterController c = (CharacterController)this.target;

        if(GUILayout.Button("Attach Ragdoll Script"))
        {

        }
    }
}
