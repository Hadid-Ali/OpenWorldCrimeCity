using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleStopPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError($"{other.gameObject} entered");
        EnterableVehicle enterableVehicle = other.GetComponent<EnterableVehicle>();

        if (enterableVehicle == null)
        {
            enterableVehicle = other.GetComponentInParent<EnterableVehicle>();
        }

        if (enterableVehicle)
        {
            enterableVehicle.ApplyStoppingBrake();
        }
    }
}
