using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePositionTween : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 EndPos;

    public void Start()
    {
       // StartPos = gameObject.GetComponent<RectTransform>();
    }

    [ContextMenu("StartValue")]
    public void GetStartValue() 
    {
        StartPos.x = this.gameObject.GetComponent<RectTransform>().position.x;
        StartPos.y = this.gameObject.GetComponent<RectTransform>().position.y;
        StartPos.z = this.gameObject.GetComponent<RectTransform>().position.z;

    }

    [ContextMenu("EndValue")]
    public void GetEndValue() 
    {
        EndPos.x = this.gameObject.GetComponent<RectTransform>().position.x;
        EndPos.y = this.gameObject.GetComponent<RectTransform>().position.y;
        EndPos.z = this.gameObject.GetComponent<RectTransform>().position.z;
    }
}
