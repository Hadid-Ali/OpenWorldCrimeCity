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


    private AdsManager _adsManager;

    void Start()
    {
        this._adsManager = AdsManager.Instance;
        this._adsManager.RequestInterstitial();
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
                    this._adsManager.ShowInterstitial();
                    modeSelection.SetActive(true);
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

    #region Mode Selection Screen

    [Header("------------ Mode Selection Screen ------------")]
    public GameObject modeSelection;

    public void ModeSelectionBtnClicked(string btnName)
    {
        switch (btnName)
        {
            case "PlayBtn":
                {
                    this._adsManager.ShowInterstitial();
                    loadingScreen.SetActive(true);
                    break;
                }
            case "ModeSelectionClosed":
                {
                    modeSelection.SetActive(false);
                    break;
                }
            default:
                break;
        }
    }

    public void ModeBtnClicked(int mode)
    {
        switch (mode)
        {
            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            default:
                break;
        }
    }

    #endregion


    #region Game Quit

    public void OnGameQuitClicked()
    {
        Application.Quit();
    }

    #endregion
}