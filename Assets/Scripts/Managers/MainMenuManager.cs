using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("------------ Common ------------")]
    [SerializeField]
    private Text[] coinsTexts;
    [SerializeField]
    private GameObject settingsPopup,loadingScreen;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (var txt in coinsTexts)
        {
            txt.text = PreferenceManager.CashBalance.ToString();
        }
    }

    #region MainMenu
    public void MainMenuButtonPressed(string btnName)
    {
        switch (btnName)
        {
            case ("Play"):
                {
                    loadingScreen.SetActive(true);
                    break;
                }
            case ("RateUs"):
                {
                    break;
                }
            case ("Shop"):
                {
                    ShopManager.Instance.OpenShop();
                    break;
                }
            case ("MoreGames"):
                {
                    break;
                }
            case ("RemoveAds"):
                {
                    break;
                }
            case ("Settings"):
                {
                    settingsPopup.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    #endregion
}