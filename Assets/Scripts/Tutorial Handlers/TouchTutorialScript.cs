using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTutorialScript : MonoBehaviour
{
    public float timer;
    public GameObject enableTouchTutorialCompletion;
    public float timerToCompleteTutorial;

    private void Start()
    {
        
    }

    void Update()
    {
        if (Mathf.Abs(InputManager.CameraInputX + InputManager.CameraInputY) > 0f)
        {
            timer += Time.deltaTime;

            if (timer > this.timerToCompleteTutorial)
            {
                this.enableTouchTutorialCompletion.SetActive(true);
                GameManager.instance.cameraManager._mainCameraController.CentralizeCamera();
                this.gameObject.SetActive(false);
            }
        }
    }
}
