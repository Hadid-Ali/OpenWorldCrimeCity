using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class PassengerPickupPoint : PassengerPoint
{
    public override void OnPlayerVehicleEntered()
    {
        GameManager.instance.taxiModule.OnRidePickedUp();
        base.OnPlayerVehicleEntered();
    }
}
