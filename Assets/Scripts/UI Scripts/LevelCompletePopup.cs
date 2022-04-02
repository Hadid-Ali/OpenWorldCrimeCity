using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelCompletePopup : MonoBehaviour
{
    public Text coinsTxt, killCount, rewardedCoinsTxt;
    public Image cpImage;

    void Start()
    {
        coinsTxt.text = "$"+PreferenceManager.CashBalance;
        killCount.text = "/";
        rewardedCoinsTxt.text = "$500";
    }

    public void UpdateCP(Sprite sprite)
    {
        cpImage.sprite = sprite;
    }

    public void HomeBtnPressed()
    {

    }

    public void RetryBtnPressed()
    {

    }

    public void NextBtnPressed()
    {

    }

    public void BackBtnPressed()
    {
        Destroy(gameObject);
    }
}