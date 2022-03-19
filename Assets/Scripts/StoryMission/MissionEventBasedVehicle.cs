using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MissionEventBasedVehicle : Vehicle
{
    public UnityEvent uEvent;

    private void OnEnable()
    {
        
    }

    public override void DestroyVehicle()
    {
        base.DestroyVehicle();
        if (this.uEvent != null)
            this.uEvent.Invoke();
    }
}
