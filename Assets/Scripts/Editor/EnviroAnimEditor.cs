using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnvironmentAnimator))]
public class EnviroAnimEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnvironmentAnimator anim = (EnvironmentAnimator)this.target;

        if(GUILayout.Button("Zombie Condition"))
        {
            anim.ApplyCondition(ConditionType.ZombieAttack);
        }
    }
}
