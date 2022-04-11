using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControls : MonoBehaviour
{

    public virtual void OnEnable()
    {
        GameManager.instance.cameraManager.TogglePlayerCamera(false);
        GameManager.instance.gameplayHUD.ToggleGameplayControls(false);
    }

    public virtual void OnDisable()
    {
        GameManager.instance.cameraManager.TogglePlayerCamera(true);
        GameManager.instance.gameplayHUD.ToggleGameplayControls(true);
    }
}
