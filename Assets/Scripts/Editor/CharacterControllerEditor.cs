using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AttackingAgent))]
public class AttackAgentEditor : CharacterControllerEditor
{

}

[CustomEditor(typeof(CharacterController))]
public class CharacterControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharacterController character = (CharacterController)this.target;

        if (GUILayout.Button("Kill Character"))
        {
            character.KillWithForce(GameManager.instance.playerController.transform.forward, 200f);
        }        
    }
}
