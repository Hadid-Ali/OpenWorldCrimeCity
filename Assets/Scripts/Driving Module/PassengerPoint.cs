using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerPoint : MonoBehaviour
{
    public string pointName = "Anonymous";
    private Vehicle v;

    private void OnTriggerEnter(Collider other)
    {
        this.v = null;
        if (other.GetComponentInParent<Vehicle>())
            this.v = other.GetComponentInParent<Vehicle>();

        if (other.GetComponent<Vehicle>())
            this.v = other.GetComponent<Vehicle>();

        if (this.v)
        {
            if (this.v.isPlayerVehicle)
            {
                this.OnPlayerVehicleEntered();
            }
        }
    }

    public virtual void OnPlayerVehicleEntered()
    {
        this.gameObject.SetActive(false);
        this.v.GetComponent<Rigidbody>().isKinematic = true;
        Invoke("NormalVehicle", 1f);
    }

    public void NormalVehicle()
    {
        this.v.GetComponent<Rigidbody>().isKinematic = false;
    }
}

