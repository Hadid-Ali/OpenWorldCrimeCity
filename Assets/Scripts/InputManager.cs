using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class InputManager : MonoBehaviour
{
    public static float CameraInputX => CnInputManager.GetAxis("CamX");
    public static float CameraInputY => CnInputManager.GetAxis("CamY");
}
