using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RagdollsPool))]
public class RagdollsPoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RagdollsPool pool = (RagdollsPool)this.target;

        for(int i=0;i<pool.ragdollEntities.Count;i++)
        {
            pool.ragdollEntities[i].name = pool.ragdollEntities[i].entityName.ToString();
        }
    }
}
