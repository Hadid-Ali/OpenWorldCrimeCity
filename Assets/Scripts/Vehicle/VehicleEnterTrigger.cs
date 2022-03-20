using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEnterTrigger : MonoBehaviour
{
    public EnterableVehicle vehicle;
    
    private void Start()
    {
        if (this.vehicle)
            this.vehicle.SetPhysicsEnable(false);
    }

    public void OnCarTriggerEntered(bool b)
    {
        GameManager.instance.gameplayHUD.ToggleCarEnterBtn(b);
        if(b)
            GameManager.instance.gameplayHUD.carEnterBtn.onClick.AddListener(this.vehicle.EnterVehicle);
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
