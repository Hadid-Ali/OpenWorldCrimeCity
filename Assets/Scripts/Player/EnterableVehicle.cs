using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnterableVehicle : Vehicle
{
    public PlayMode playType = PlayMode.CAR;

    public bool isUberVehicle = false;

    public GameObject dummyDriver;

    public Transform[] ikTargets;

    // Start is called before the first frame update
    public virtual void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    public void ToggleDummyDriver(bool b)
    {
        if (this.dummyDriver)
            this.dummyDriver.SetActive(b);
    }

    public abstract void OnVehicleEntered();
    public abstract void OnVehicleExit();
}
