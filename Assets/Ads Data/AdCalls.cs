
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using GoogleMobileAds.Common;

public enum AdSizes
{
    Banner, SmartBanner
}

public enum AdStatus
{
    NotLoading,
    Loading,
    Loaded,
    Shown,
    Request_Given,
    Request_Not_Given
}

public static class PluginPreReqs
{
    public static bool IsInternetAvailable => Application.internetReachability != NetworkReachability.NotReachable;

    public static bool IsAdsCapable => IsValidSDK() && IsValidDevice();

    private static bool _isValidSdkInitialized = false;
    private static bool _isValidSDK;

    private static bool _isDeviceSpecsCalculated = false;
    private static bool _isValidDevice = false;

    public static bool IsValidSDK()
    {
#if UNITY_ANDROID
        if(!_isValidSdkInitialized)
        {
            string info = SystemInfo.operatingSystem;

            string sdkversion = info.Substring(0, 16);
            if (sdkversion.Equals("Android OS 8.1.0") && SystemInfo.systemMemorySize < 2048)
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent("InValidSDK");

                _isValidSDK =  false;
            }
            else
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent("ValidSDK");
                _isValidSDK =  true;
            }
#elif UNITY_IPHONE
            _isValidSDK =  true;
#endif
            _isValidSdkInitialized = true;
        }

        return _isValidSDK;
    }


    public static bool IsValidDevice()
    {
#if UNITY_ANDROID
        if(!_isDeviceSpecsCalculated)
        {
            if (SystemInfo.systemMemorySize < 1024)
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent("InValidDevice");

                _isValidDevice = false;
            }
            else
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent("ValidDevice");

                _isValidDevice =  true;
            }
#elif UNITY_IPHONE
            _isValidDevice =  true;
#endif
            _isDeviceSpecsCalculated = true;
        }
        return _isValidDevice;
    }
}

public class AdCalls : MonoBehaviour, IUnityAdsListener
{
    string reward;
    public static Action<string> OnRewardCompleted;
    public static AdCalls instance;
    private InterstitialAd interstitial;
    private BannerView bannerView1;
    private BannerView bannerView2;
    private BannerView bannerView3;
    private RewardedAd rewardedAd;
    public GameObject RawImg;
    [Header("Unity Id")]
    public string UnityIdIOS, UnityIdAndroid;
    string myPlacementId = "rewardedVideo";

    [Header("Admob Ad Ids")]
    public string Admob_Interstitial_Android;
    public string Admob_Banner_Android1;
    public string Admob_Banner_Android2;
    public string Admob_Rewarded_Android;
    [Space(20)]
    public string Admob_Interstitial_IOS;
    public string Admob_Banner_IOS;
    public string Admob_Rewarded_IOS;

    [Header("Banner Type")]
    public AdSizes _bannerType;

    [Header("Banner Postition")]
    public AdPosition _bannerPosition1;
    public AdPosition _bannerPosition2;

    public bool TestAds, TwoBanners;

    private Action rewardedDelegate;

    private AdStatus _interstitialStatus;
    private AdStatus _rewardedStatus;

    #region ----------------------- Start --------------------------
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        if (TestAds)
        {
            Admob_Interstitial_Android = Admob_Interstitial_IOS = "ca-app-pub-3940256099942544/1033173712";
            Admob_Banner_Android1 = Admob_Banner_Android2 = Admob_Banner_IOS = "ca-app-pub-3940256099942544/6300978111";
            Admob_Rewarded_Android = Admob_Rewarded_IOS = "ca-app-pub-3940256099942544/5224354917";
        }
        Advertisement.AddListener(this);
        if (PlayerPrefs.GetInt("removeads") != 1)
        {

            #region Unity
#if UNITY_ANDROID
            if (PluginPreReqs.IsAdsCapable)
            {
                Advertisement.Initialize(UnityIdAndroid, TestAds);
            }

#elif UNITY_IOS
                Advertisement.Initialize(UnityIdIOS, TestAds);
#endif
            #endregion

            #region Admob

            if (!PluginPreReqs.IsAdsCapable)
            {
                return;
            }
            MobileAds.Initialize(initStatus =>
            {
                CreateAndLoadRewardedAd();
                if (PlayerPrefs.GetInt("removeads") != 1)
                {
                    if (cpManager.instance)
                    {
                        if (IsAdEnabled())
                        {
                            RequestInterstitial();
                            if (IsBannerEnabled())
                            {
                                RequestBanner1();
                                if (TwoBanners)
                                    RequestBanner2();
                            }
                        }
                    }
                    else
                    {
                        RequestInterstitial();
                        RequestBanner1();
                        if (TwoBanners)
                            RequestBanner2();
                    }
                }
            });

            #endregion
        }
    }


    #endregion
    public static bool IsBannerEnabled()
    {
        if (cpManager.instance)
        {
            if (cpManager.instance.isAdBackendCall)
            {
                if (cpManager.instance.getBannerStatus == "true" || cpManager.instance.getBannerStatus == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }
    public static bool IsAdEnabled()
    {
        if (cpManager.instance)
        {
            if (cpManager.instance.isAdBackendCall)
            {
                if (cpManager.instance.versionID < cpManager.instance.AdsVersionID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    #region --------------Admob Functionality------------------
    private void RequestInterstitial()
    {
        if (!PluginPreReqs.IsAdsCapable || !PluginPreReqs.IsInternetAvailable || this._interstitialStatus == AdStatus.Request_Given)
        {
            return;
        }

#if UNITY_ANDROID
        string adUnitId = Admob_Interstitial_Android;
#elif UNITY_IPHONE
        string adUnitId = Admob_Interstitial_IOS;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new GoogleMobileAds.Api.InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        this._interstitialStatus = AdStatus.Request_Given;

        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;

    }
    public void CreateAndLoadRewardedAd()
    {
        if (!PluginPreReqs.IsInternetAvailable || !PluginPreReqs.IsAdsCapable || this._rewardedStatus == AdStatus.Request_Given)
        {
            return;
        }

#if UNITY_ANDROID
        string adUnitId = Admob_Rewarded_Android;
#elif UNITY_IPHONE
        string adUnitId = Admob_Rewarded_IOS;
#else
        string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
        this._rewardedStatus = AdStatus.Request_Given;

    }

    private void RequestBanner1()
    {
        if (!PluginPreReqs.IsValidSDK() || !PluginPreReqs.IsInternetAvailable)
        {
            return;
        }

#if UNITY_ANDROID
        string adUnitId = Admob_Banner_Android1;
#elif UNITY_IPHONE
            string adUnitId = Admob_Banner_IOS;
#else
            string adUnitId = "unexpected_platform";
#endif
        Debug.Log("Banner Ad 1 Req");
        // Create a 320x50 banner at the top of the screen.
        bannerView1 = new BannerView(adUnitId, _bannerType == AdSizes.Banner ? AdSize.Banner : AdSize.SmartBanner, _bannerPosition1);
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView1.LoadAd(request);
    }
    private void RequestBanner2()
    {
        if (!PluginPreReqs.IsValidSDK() || !PluginPreReqs.IsInternetAvailable)
        {
            return;
        }

#if UNITY_ANDROID
        string adUnitId = Admob_Banner_Android2;
#elif UNITY_IPHONE
            string adUnitId = Admob_Banner_IOS;
#else
            string adUnitId = "unexpected_platform";
#endif

        Debug.Log("Banner Ad 2 Req");
        // Create a 320x50 banner at the top of the screen.
        bannerView2 = new BannerView(adUnitId, _bannerType == AdSizes.Banner ? AdSize.Banner : AdSize.SmartBanner, _bannerPosition2);
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView2.LoadAd(request);
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() => 
        {
            this.RewardUser();
        });
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            this.CreateAndLoadRewardedAd();
        });
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            this.CreateAndLoadRewardedAd();
        });
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            RequestInterstitial();
        });
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            RequestInterstitial();
        });
    }
    #endregion

    #region --------------Unity Functionality------------------

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            RewardUser();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }
    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    #endregion

    #region ------------------- Ad Calling Functions--------------------
    public void RemoveBanner()
    {
        if (!PluginPreReqs.IsValidSDK() || !PluginPreReqs.IsInternetAvailable)
        {
            return;
        }
        try
        {
            if (bannerView1 != null)
                bannerView1.Destroy();
            if (bannerView2 != null)
                bannerView2.Destroy();
        }
        catch (Exception ex)
        {
            if (AppMetrica.Instance != null)
                AppMetrica.Instance.ReportEvent(ex.Message);
            Debug.Log(ex.Message.ToString());
        }

    }


    public void CallAdmob()
    {
        if (!PluginPreReqs.IsAdsCapable || !PluginPreReqs.IsInternetAvailable)
        {

            return;
        }
        if (PlayerPrefs.GetInt("removeads") != 1)
        {
            try
            {
                if (IsAdEnabled())
                {
                    if (this.interstitial.IsLoaded())
                    {
                        this.interstitial.Show();
                    }
                    else
                    {
                        RequestInterstitial();
                    }
                }
            }
            catch (Exception ex)
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent("CallAdmob " + ex.Message);
                Debug.Log(ex.Message.ToString());
            }


        }
    }
    public void Admob_Unity()
    {
        if (!PluginPreReqs.IsValidSDK())
        {
            return;
        }

        if (PlayerPrefs.GetInt("removeads") != 1)
        {
            try
            {
                if (IsAdEnabled())
                {
                    if (PluginPreReqs.IsValidDevice())
                    {
                        if (this.interstitial.IsLoaded())
                        {
                            this.interstitial.Show();
                        }
                        else
                        {
                            RequestInterstitial();
                            if (Advertisement.IsReady())
                            {
                                Advertisement.Show();
                            }
                        }
                    }
                    else
                    {
                        if (this.interstitial.IsLoaded())
                        {
                            this.interstitial.Show();
                        }
                        else
                        {
                            RequestInterstitial();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent("Admob_Unity " + ex.Message);
            }

        }
    }
    public void Unity_Admob()
    {
        if (!PluginPreReqs.IsValidSDK() || !PluginPreReqs.IsInternetAvailable)
        {
            return;
        }
        if (PlayerPrefs.GetInt("removeads") != 1)
        {
            try
            {
                if (IsAdEnabled())
                {
                    if (PluginPreReqs.IsValidDevice())
                    {
                        if (Advertisement.IsReady())
                        {
                            Advertisement.Show();
                        }
                        else
                        {
                            if (this.interstitial.IsLoaded())
                            {
                                this.interstitial.Show();
                            }
                            else
                            {
                                RequestInterstitial();
                            }
                        }
                    }
                    else
                    {
                        if (this.interstitial.IsLoaded())
                        {
                            this.interstitial.Show();
                        }
                        else
                        {
                            RequestInterstitial();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent(ex.Message);
                Debug.Log(ex.Message.ToString());
            }



        }
    }
    public void RewardVideo(Action _reward)
    {
        if (!PluginPreReqs.IsValidSDK())
        {
            RewardUser();
            return;
        }

        try
        {
            this.rewardedDelegate = _reward;
#if UNITY_EDITOR
            RewardUser();
#endif

            if (PluginPreReqs.IsValidDevice())
            {
                if (this.rewardedAd.IsLoaded())
                {
                    this.rewardedAd.Show();
                }
                else
                {

                    if (Advertisement.IsReady(myPlacementId))
                    {
                        Advertisement.Show(myPlacementId);
                    }
                    this.CreateAndLoadRewardedAd();
                }
            }
            else
            {
                if (this.rewardedAd.IsLoaded())
                {
                    this.rewardedAd.Show();
                }
                else
                {
                    this.CreateAndLoadRewardedAd();
                }
            }

        }
        catch (Exception ex)
        {
            if (AppMetrica.Instance != null)
                AppMetrica.Instance.ReportEvent("RewardVideo " + ex.Message);
            Debug.Log(ex.Message.ToString());
        }


    }
    public void UnityInterstitial()
    {
        if (!PluginPreReqs.IsValidSDK())
        {
            return;
        }

        if (PlayerPrefs.GetInt("removeads") != 1)
        {
            try
            {
                if (PluginPreReqs.IsValidDevice())
                {
                    if (Advertisement.IsReady())
                    {
                        Advertisement.Show();
                    }
                }
                else
                {
                    if (this.interstitial.IsLoaded())
                    {
                        this.interstitial.Show();
                    }
                    else
                    {
                        RequestInterstitial();
                    }
                }

            }
            catch (Exception ex)
            {
                if (AppMetrica.Instance != null)
                    AppMetrica.Instance.ReportEvent("UnityInterstitial " + ex.Message);
                Debug.Log(ex.Message.ToString());
            }

        }
    }
    #endregion
    void RewardUser()
    {
        try
        {
            if (this.rewardedDelegate != null)
                this.rewardedDelegate();
        }
        catch(System.Exception e)
        {
            Debug.LogError($"Exception : {e.ToString()}");
        }
    }
}
