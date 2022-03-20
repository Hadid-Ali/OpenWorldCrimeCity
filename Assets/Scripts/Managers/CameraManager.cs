using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameManager _gameManager;

    public Camera _mainCamera;
    public vThirdPersonCamera _mainCameraController;

    public RCCCarCamera _vehicleCamera;

    [SerializeField]
    private RCCCamManager _vehiclelCameraModeManager;

    [SerializeField]
    private CameraShake _cameraShake;

    public void EnableVehicleCamera(GameObject vehicle,bool shouldAllowOrbit)
    {
        this._vehiclelCameraModeManager.useOrbitCamera = shouldAllowOrbit;
        this._vehiclelCameraModeManager.useFixedCamera = !shouldAllowOrbit;

        this._vehicleCamera.playerCar = vehicle.transform;

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
