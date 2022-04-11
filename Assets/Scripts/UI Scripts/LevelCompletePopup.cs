using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelCompletePopup : LevelEndPanel
{
    [SerializeField]
    private GameObject normalCompleteMenu;

    [SerializeField]
    private GameObject gameCompleteMenu;

    public override void OnLevelEndPanelShow()
    {
        bool isLastLevel = Constant.GameplayData.IsLastLevel;

        this.normalCompleteMenu.SetActive(isLastLevel);
        this.gameCompleteMenu.SetActive(!isLastLevel);
    }

    public void NextMission()
    {
        GameManager.instance.StartNextMission();
    }
}