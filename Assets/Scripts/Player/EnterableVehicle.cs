using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnterableVehicle : Vehicle
{
    public ControlsMode controlsType = ControlsMode.CAR;
    public GameObject dummyDriver;

    public float lowSpeedLimit = 2f;

    public override void Start()
    {
        base.Start();
    }

    public virtual void SetPhysicsEnable(bool toggle)
    {
        if (this.rb == null)
            this.TryGetComponent<Rigidbody>(out this.rb);
        this.rb.isKinematic = !toggle;
    }

    public virtual float VehicleVeloctiy => Mathf.Abs(this.rb.velocity.magnitude);
    public bool IsVehicleAtLowSpeed => this.VehicleVeloctiy <= this.lowSpeedLimit;

    public virtual void SwitchToCar(bool b)
    {
        this.ToggleDummyDriver(b);

        // GameManager.instance.playerController.fullBodyIK.SetIKTargets(this.vehicle.ikTargets);
        GameManager.instance.cameraManager.EnableVehicleCamera(this.gameObject,true);
        this.SetPhysicsEnable(true);
        GameManager.instance.ChangePlayMode(b ? this.controlsType : ControlsMode.PLAYER);
        GameManager.instance.playerController.transform.SetParent(b ? this.transform : null);
        GameManager.instance.gameplayHUD.ToggleCarEnterBtn(!b);
        GameManager.instance.gameplayHUD.ToggleCarExitBtn(b);
        GameManager.instance.playerController.gameObject.SetActive(!b);


        Vector3 v = GameManager.instance.playerController.transform.eulerAngles;

        GameManager.instance.playerController.AllignWith(this.transform);

        if (b)
        {
            GameManager.instance.gameplayHUD.carExitBtn.onClick.AddListener(this.ExitVehicle);
            GameManager.instance.playerVehicle = this;
       //     StartCoroutine(this.SpeedCheckRoutine());
        }

        else
        {
            StopAllCoroutines();
            GameManager.instance.playerVehicle = null;
            GameManager.instance.gameplayHUD.carExitBtn.onClick.RemoveAllListeners();
        }
    }

    public void EnterVehicle()
    {
        this.OnVehicleEntered();
        this.SwitchToCar(true);
    }

    public void ExitVehicle()
    {
        this.OnVehicleExit();
        this.SwitchToCar(false);
    }

    public void ToggleDummyDriver(bool b)
    {
        if (this.dummyDriver)
            this.dummyDriver.SetActive(b);
    }

    public abstract void OnVehicleEntered();
    public abstract void OnVehicleExit();
}
