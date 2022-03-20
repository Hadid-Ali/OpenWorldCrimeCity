using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public vThirdPersonCamera characterCam;
    public RCCCarCamera vehicleCam;

    public void SetVehicleCamTarget(Transform T)
    {
        this.vehicleCam.playerCar = T;
    }

    public void ToggleCamMode(ControlsMode mode)
    {
        this.characterCam.enabled = false;
        this.vehicleCam.enabled = false;
        switch(mode)
        {
            case ControlsMode.PLAYER:
                this.characterCam.enabled = true;
                break;

            case ControlsMode.CAR:
                this.vehicleCam.enabled = true;
                break;

            case ControlsMode.BIKE:
                this.vehicleCam.enabled = true;
                break;
        }
    }
}
