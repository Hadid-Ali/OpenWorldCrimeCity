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

    public void ToggleCamMode(PlayMode mode)
    {
        this.characterCam.enabled = false;
        this.vehicleCam.enabled = false;
        switch(mode)
        {
            case PlayMode.PLAYER:
                this.characterCam.enabled = true;
                break;

            case PlayMode.CAR:
                this.vehicleCam.enabled = true;
                break;

            case PlayMode.BIKE:
                this.vehicleCam.enabled = true;
                break;
        }
    }
}
