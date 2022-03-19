using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerDropOffPoint : PassengerPoint
{
    public override void OnPlayerVehicleEntered()
    {
        GameManager.instance.taxiModule.OnRideDroppedOff();
        base.OnPlayerVehicleEntered();
    }
}
