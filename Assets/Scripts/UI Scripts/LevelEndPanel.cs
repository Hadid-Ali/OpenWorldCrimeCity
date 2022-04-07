using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class LevelEndPanel : MonoBehaviour
{
    public Text totalKills;
    public Text totalCashEarning;

    private void OnEnable()
    {
        this.OnLevelEndPanelShow();
    }

    public virtual void OnLevelEndPanelShow()
    {
        this.totalKills.text = GameManager.instance.totalKills.ToString ();
        this.totalCashEarning.text = $"{GameManager.instance.totalCashEarned}$";
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }

    public void HomeButton()
    {
        GameManager.instance.QuitToMainMenu();
    }

}
