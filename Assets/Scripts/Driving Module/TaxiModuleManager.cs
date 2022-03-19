using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiModuleManager : MonoBehaviour
{
    public List<PassengerPickupPoint> pickupPoints;
    public List<PassengerDropOffPoint> dropOffPoints;

    public List<GameObject> passengers;

    private GameObject currentPassenger;
    private PassengerPickupPoint currentPickUpPoint;
    private PassengerDropOffPoint currentDropOffPoint;

    public float peakRate = 1f,travelDistance = 0f;

    private int currentFare = 0, travelFare = 10;

    public float minRideWait = 5f, maxRideWait = 300f;

    public bool isDrivingCurrently = false;

    public void OnRidePickedUp()
    {
        this.currentPickUpPoint.gameObject.SetActive(false);
        this.currentDropOffPoint.gameObject.SetActive(true); GameManager.instance.gameplayHUD.ShowMessage(string.Format("Drop Rider at {0} Location",this.currentDropOffPoint.pointName), "Ride", MessageMode.Ok);
    }

    public void RideOfferDeclined()
    {
        this.isDrivingCurrently = false;
    }

    public void RideOfferAccepted()
    {
        this.StartRideLoop();
    }

    public void StartRideLoop()
    {
        float wait = this.isDrivingCurrently ? Random.Range(this.minRideWait, this.maxRideWait) : 0;
        this.isDrivingCurrently = true;
        Invoke("MakeRide", wait);
    }

    public void NullifyRide()
    {
        this.currentFare = 0;
        this.currentPassenger = null;

        this.currentPickUpPoint = null;
        this.currentDropOffPoint = null;

        this.peakRate = 1;
        this.travelDistance = 0f;

        this.currentFare = 0;
    }

    public void MakeRide()
    {
        this.currentDropOffPoint = this.dropOffPoints[Random.Range(0, this.dropOffPoints.Count)];
        this.currentPickUpPoint = this.pickupPoints[Random.Range(0, this.pickupPoints.Count)];

        this.currentPassenger = this.passengers[Random.Range(0, this.passengers.Count)];

        this.currentPassenger.SetActive(true);
        this.currentPassenger.transform.position = this.currentPickUpPoint.transform.position;

        this.currentPickUpPoint.gameObject.SetActive(true);

        this.peakRate = Random.Range(0f, 10f) > 4 ? 1f : Random.Range(1f, 3f);

        this.travelDistance = Vector3.Distance(this.currentPickUpPoint.transform.position, this.currentDropOffPoint.transform.position);

        this.currentFare = (int) ((this.travelDistance * this.travelFare) * this.peakRate);

        GameManager.instance.gameplayHUD.ShowMessage(string.Format("Pick Ride From {0} and drop at {1}",this.currentPickUpPoint.pointName,this.currentDropOffPoint.pointName),"Ride",MessageMode.Ok);
    }


    public void OnRideDroppedOff()
    {
        GameManager.instance.gameplayHUD.ShowMessage(string.Format("Ride Completed Earned {0}", this.currentFare), "Ride Completed", MessageMode.Ok, null, null, this.AskForFurtherRides);
        this.currentDropOffPoint.gameObject.SetActive(false);
        Debug.LogError(this.currentFare);
        this.NullifyRide();
    }

    public void AskForFurtherRides()
    {
        GameManager.instance.gameplayHUD.ShowMessage("Do You Want to Keep Earning by Driving?", "Earn Cash", MessageMode.YesNo, GameManager.instance.taxiModule.RideOfferAccepted, GameManager.instance.taxiModule.RideOfferDeclined);
    }
}
