using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCarHandler : MonoBehaviour
{
    public GameObject carObject;

    private bool once;

    private void OnEnable()
    {
        once = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAGS.PLAYER) && !once)
        {
            once = true;
            TrafficToRccCar.Instance.ShowEnterCarBtn(carObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        once = false;
        TrafficToRccCar.Instance.HideEnterCarBtn();
    }
}