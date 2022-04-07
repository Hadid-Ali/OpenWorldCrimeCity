using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelCompletePopup : LevelEndPanel
{
    public GameObject nextButton;

    public override void OnLevelEndPanelShow()
    {
        this.nextButton.SetActive(!Constant.GameplayData.IsLastLevel);
    }

    public void NextMission()
    {
        GameManager.instance.StartNextMission();
    }
}