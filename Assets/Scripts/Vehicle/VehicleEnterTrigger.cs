using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEnterTrigger : MonoBehaviour
{
    public EnterableVehicle vehicle;
    public VehicleCameraHandler vehicleCamHandler;
    private Rigidbody rb;
    
    private void Start()
    {
        if(this.GetComponentInParent<EnterableVehicle>())
        this.vehicle = this.GetComponentInParent<EnterableVehicle>();
        this.vehicle.enabled = false;

        if(this.GetComponentInParent<VehicleCameraHandler>())
        {
            this.vehicleCamHandler = this.GetComponentInParent<VehicleCameraHandler>();
        }
        
        this.rb = this.GetComponentInParent<Rigidbody>();

        if (this.rb)
            this.rb.isKinematic = true;
    }

    void SwitchToCar(bool b)
    {
        if (this.vehicle)
        {
            this.vehicle.enabled = b;
            this.vehicle.ToggleDummyDriver(b);
        }

        this.rb.isKinematic = !b;


      // GameManager.instance.playerController.fullBodyIK.SetIKTargets(this.vehicle.ikTargets);
        GameManager.instance.camController.SetVehicleCamTarget(this.vehicle.transform);
        GameManager.instance.ChangePlayMode(b ? this.vehicle.playType : PlayMode.PLAYER);
        GameManager.instance.playerController.transform.SetParent(b ? this.transform : null);
        GameManager.instance.gameplayHUD.ToggleCarEnterBtn(!b);
        GameManager.instance.gameplayHUD.ToggleCarExitBtn(b);
        GameManager.instance.playerController.gameObject.SetActive(!b);


        Vector3 v = GameManager.instance.playerController.transform.eulerAngles;

        GameManager.instance.playerController.transform.eulerAngles = new Vector3(0f, v.y, 0f);

        if (this.vehicleCamHandler)
            GameManager.instance.camController.vehicleCam.ChangeCamProperties(this.vehicleCamHandler);

        if (b)
        {
            GameManager.instance.gameplayHUD.carExitBtn.onClick.AddListener(this.ExitVehicle);
            GameManager.instance.playerVehicle = this.vehicle;
            StartCoroutine(this.SpeedCheckRoutine());
        }

        else
        {
            StopAllCoroutines();
            GameManager.instance.playerVehicle = null;
            GameManager.instance.gameplayHUD.carExitBtn.onClick.RemoveAllListeners();
        }
        
    }

    public IEnumerator SpeedCheckRoutine()
    {
        while(true)
        {
            bool canstop = Mathf.Abs(this.rb.velocity.magnitude) < 2f;
            GameManager.instance.gameplayHUD.ToggleCarExitBtn(canstop);

            yield return new WaitForSeconds(1f);
        }
    }

    public void EnterVehicle()
    {
        this.vehicle.OnVehicleEntered();
        this.SwitchToCar(true);
    }

    public void ExitVehicle()
    {
        if (this.vehicle)
            this.vehicle.OnVehicleExit();
        this.SwitchToCar(false);

    }

    public void OnCarTriggerEntered(bool b)
    {
        GameManager.instance.gameplayHUD.ToggleCarEnterBtn(b);
        if(b)
            GameManager.instance.gameplayHUD.carEnterBtn.onClick.AddListener(this.EnterVehicle);
        else
            GameManager.instance.gameplayHUD.carEnterBtn.onClick.RemoveAllListeners();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAGS.PLAYER))
        {
            this.OnCarTriggerEntered(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(Constant.TAGS.PLAYER))
        {
            this.OnCarTriggerEntered(false);
        }
    }
}
