using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficToRccCar : MonoBehaviour
{
    public GameplayHUD HUD;
    public RCC_Camera cam;
    public GameObject rccCar;
    public GameObject playerCam, player, controlsCanvas, trafficCar;

    public static TrafficToRccCar Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public bool EnterTrafficCar()
    {
        if (trafficCar && trafficCar.activeInHierarchy)
        {
            Vector3 pos = trafficCar.transform.position + Vector3.up;
            Quaternion rot = trafficCar.transform.rotation;
            trafficCar.SetActive(false);
            var car = Instantiate(rccCar) as GameObject;
            car.transform.rotation = rot;
            car.transform.position = pos;
            //car.transform.localScale = Vector3.one * 0.5f;
            cam.playerCar = car.GetComponent<RCC_CarControllerV3>();
            cam.gameObject.SetActive(true);

            controlsCanvas.SetActive(true);
            player.SetActive(false);
            playerCam.SetActive(false);
            return true;
        }
        return false;
    }

    public void ShowEnterCarBtn(GameObject carObject)
    {
        trafficCar = carObject;
        HUD.ShowCarButton();
    }

    public void HideEnterCarBtn()
    {
        trafficCar = null;
        HUD.HideCarButton();
    }
}