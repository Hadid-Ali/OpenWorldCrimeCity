using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void Restart()
    {
        GameManager.instance.ShowInterstitial();
        GameManager.instance.RestartGame();
    }

    public void Resume()
    {
        GameManager.instance.ResumeGame();
    }

    public void Home()
    {
        GameManager.instance.ShowInterstitial();
        GameManager.instance.QuitToMainMenu();
    }
}
