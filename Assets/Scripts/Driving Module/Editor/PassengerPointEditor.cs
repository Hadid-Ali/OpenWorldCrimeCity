using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PassengerPoint),true)]
public class PassengerPointEditor :Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PassengerPoint point = (PassengerPoint)this.target;

        RaycastHit hit;

        if(Physics.Raycast(point.transform.position,-point.transform.up,out hit,LayerMask.GetMask("EnvTerrain")))
        {
            point.transform.position = new Vector3(point.transform.position.x, hit.transform.position.y, point.transform.position.z);
        }
    }
}
