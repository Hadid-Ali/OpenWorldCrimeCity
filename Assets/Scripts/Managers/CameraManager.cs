using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameManager _gameManager;

    public Camera _mainCamera;
    public vThirdPersonCamera _mainCameraController;

    public RCCCameraOrbit _vehicleCamera;

    public void EnableVehicleCamera(GameObject vehicle)
    {
        this._vehicleCamera.target = vehicle.transform;
        this.TogglePlayerCamera(false);
        this.ToggleVehicleCamera(true);
    }

    public void EnablePlayerCamera(GameObject player)
    {
        this._mainCameraController.SetMainTarget(player.transform);
        this.ToggleVehicleCamera(false);
        this.TogglePlayerCamera(true);
    }

    public void ToggleVehicleCamera(bool toggle)
    {
        this._vehicleCamera.enabled = toggle;
    }

    public void TogglePlayerCamera(bool toggle)
    {
        this._mainCameraController.enabled = toggle;
    }

}
