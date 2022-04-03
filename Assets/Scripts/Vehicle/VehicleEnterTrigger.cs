using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class VehicleEnterTrigger : MonoBehaviour
{
    public EnterableVehicle vehicle;
    
    [SerializeField]
    private bool canAutomaticEnter = false;

    [SerializeField]
    private bool isSingleEntrance = false;

    public UnityEvent onVehicleEntered;

    private void Start()
    {
        if (this.vehicle)
            this.vehicle.SetPhysicsEnable(false);
    }

    public void OnCarTriggerEntered(bool b)
    {
        if(b & this.canAutomaticEnter)
        {
            this.EnterVehicle();
            return;
        }

        GameManager.instance.gameplayHUD.ToggleCarEnterBtn(b);
        if(b)
            GameManager.instance.gameplayHUD.carEnterBtn.onClick.AddListener(this.EnterVehicle);
        else
            GameManager.instance.gameplayHUD.carEnterBtn.onClick.RemoveAllListeners();
    }

    public void EnterVehicle()
    {
        this.vehicle.EnterVehicle();

        if (this.onVehicleEntered != null)
            this.onVehicleEntered.Invoke();

        this.gameObject.SetActive(!isSingleEntrance);
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
