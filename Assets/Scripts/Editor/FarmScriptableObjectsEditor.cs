using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FarmScriptableObjects))]
public class FarmScriptableObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FarmScriptableObjects farm = (FarmScriptableObjects)this.target;
        farm.name = farm.farmName.ToString();
    }
    
}
