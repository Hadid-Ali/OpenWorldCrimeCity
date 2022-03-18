using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(iTSSizer))]
public class LaneSizer : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        iTSSizer sizer = (iTSSizer)this.target;

        if(GUILayout.Button("Resize Lanes"))
        {
            sizer.SizeLanes();
        }

        if(GUILayout.Button("Init"))
        {
            sizer.Start();
        }

        if(GUILayout.Button("AddOffset"))
        {
            sizer.AddOffsetToNewLanes();
        }
    }
}
