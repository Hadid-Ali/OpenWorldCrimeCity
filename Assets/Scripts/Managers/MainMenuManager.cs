using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [Header("------------ Common ------------")]
    [SerializeField]
    private Text[] coinsTexts;

    [SerializeField]
    private GameObject settingsPopup,loadingScreen,mainMenu;

    private AdCalls _adsManager;

    private void Awake()
    {
        Instance = this;
        this._adsManager = AdCalls.instance;
    }

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
                    this.mainMenu.SetActive(false);
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
                    this._adsManager.Admob_Unity();
                    loadingScreen.SetActive(true);
                    break;
                }
            case "ModeSelectionClosed":
                {
                    this.mainMenu.SetActive(true);
                    modeSelection.SetActive(false);
                    break;
                }
            default:
                break;
        }
    }

    public void OnLevelSelected()
    {
        //this._adsManager.ShowInterstitial();
        AdCalls.instance.Admob_Unity();
        loadingScreen.SetActive(true);
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