using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameManager _gameManager;

    public Camera _mainCamera;
    public vThirdPersonCamera _mainCameraController;

    public RCC_Camera _vehicleCamera;

    [SerializeField]
    private CameraShake _cameraShake;

    public void EnableVehicleCamera(VehicleCameraProperties vehicle)
    {

        this._vehicleCamera.playerCar = vehicle.GetComponent<RCC_CarControllerV3>();
        this._vehicleCamera.pivot = vehicle.vehiclePivotToAssign;

        this.TogglePlayerCamera(false);
        this.ToggleVehicleCamera(true);
    }

    public void EnablePlayerCamera(GameObject player)
    {
        this._mainCameraController.SetMainTarget(player.transform);
        this.ToggleVehicleCamera(false);
        this.TogglePlayerCamera(true);
    }

    public void ShakeMainCamera()
    {
        if (this._cameraShake)
            this._cameraShake.enabled = true;
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
