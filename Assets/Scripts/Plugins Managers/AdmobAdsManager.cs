using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

//using GoogleMobileAds.Mediation;
using GoogleMobileAds.Common;
//using GoogleMobileAds.Common.Mediation.Tapjoy;
//using GoogleMobileAds.Api.Mediation.Tapjoy;
//using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation;

//using GoogleMobileAds.Api.Mediation.Vungle;


//using GoogleMobileAds.Common.Mediation.UnityAds;
//using GoogleMobileAds.Common.Mediation.AppLovin;
//using GoogleMobileAds.Api.Mediation.AppLovin;



[System.Serializable]
public class AdIDsTemplate
{

    public string appID;
    public string interstitialID;
    public string rewardedAdID;
    public string bannerAdID;
}

[System.Serializable]
public class AdIds
{
    [SerializeField]
    private AdIDsTemplate androidAdIDs;

    [SerializeField]
    private AdIDsTemplate iosAdIDs;

    public string AppID
    {
        get
        {
            string appIDToReturn;
#if UNITY_ANDROID
            appIDToReturn =  this.androidAdIDs.appID;
#else
            appIDToReturn =  this.iosAdIDs.appID;
#endif
            return appIDToReturn.Trim();
        }
    }

    public string InterstitialID
    {
        get
        {
            string interstitialIDToReturn;
#if UNITY_ANDROID
            interstitialIDToReturn = this.androidAdIDs.interstitialID;
#else
            interstitialIDToReturn =  this.iosAdIDs.interstitialID;
#endif
            return interstitialIDToReturn.Trim();
        }
    }

    public string RewardedAdID
    {
        get
        {
            string rewardedAdIDToReturn;
#if UNITY_ANDROID
            rewardedAdIDToReturn = this.androidAdIDs.rewardedAdID;
#else
            rewardedAdIDToReturn =  this.iosAdIDs.rewardedAdID;
#endif
            return rewardedAdIDToReturn.Trim();
        }
    }

    public string BannerAdID
    {
        get
        {
            string bannerAdIDToReturn;
#if UNITY_ANDROID
            bannerAdIDToReturn = this.androidAdIDs.bannerAdID;
#else
            bannerAdIDToReturn =  this.iosAdIDs.bannerAdID;
#endif
            return bannerAdIDToReturn.Trim();
        }
    }

}


public class AdmobAdsManager : AdsManager
{
    [SerializeField]
    private AdIds adIDs;

    private string appID;
    private string interstitialID;
    private string rewardedID;
    private string bannerID;

    public bool isPluginInitialized = false;

    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private BannerView bannerAd;

    public Action rewardedDelegate;
    public Action actionOnAdShown;
    public Action actionOnAdNotShown;

    private bool isInterstitialRequestGiven = false;
    private AdStatus bannerAdStatus = AdStatus.NotLoading;
    private bool isRewardedRequestGiven = false;
    private bool isAdmobInitialized = false;

    private bool isLinkerAd = false;

   // private GameplayDataManager gameplayDataManager;
    private BannerPosition requiredBannerPosition;

    void Start()
    {
        DontDestroyOnLoad(this);

        this.InitializeAdIDs();

     //   gameplayDataManager = GameplayDataManager.Instance;
        this.InitializePlugin();
        MobileAds.SetiOSAppPauseOnBackground(true);
    }

    private void InitializeAdIDs()
    {
        this.appID = this.adIDs.AppID;
        this.interstitialID = this.adIDs.InterstitialID;
        this.rewardedID = this.adIDs.RewardedAdID;
        this.bannerID = this.adIDs.BannerAdID;
    }

    public override void InitializePlugin()
    {
       // GALogger.LogAdmobStatusEvent(PluginEventType.Initialize);
        MobileAds.Initialize(this.OnAdmobInit);
    }

    void OnAdmobInit(InitializationStatus initializationStatus)
    {
        Dictionary<string, AdapterStatus> adapterStates = initializationStatus.getAdapterStatusMap();

        foreach(KeyValuePair<string,AdapterStatus> adapter in adapterStates)
        {
            string className = adapter.Key;
            AdapterStatus adapterStatus = adapter.Value;

            switch (adapterStatus.InitializationState)
            {
                case AdapterState.Ready:
                    this.SetAdapterConsent(className);
                    break;

                case AdapterState.NotReady:
                    
                    break;
            }

        }

        this.isAdmobInitialized = true;
    //    GALogger.LogAdmobStatusEvent(PluginEventType.Initialized);
        MobileAdsEventExecutor.ExecuteInUpdate(this.RequestRewardedVideo);
        //MobileAdsEventExecutor.ExecuteInUpdate(()=> 
        //{
        //    this.RequestBannerAd(true,BannerPosition.Bottom);
        //});
    }

    public override bool IsRewardedAdReady => this.rewardedAd != null && this.rewardedAd.IsLoaded();

    public override bool IsInterstitialAdReady => this.interstitialAd != null && this.interstitialAd.IsLoaded();
    public override bool IsBannerAdReady => this.bannerAd != null;
    //   public  bool IsSecondaryRewardedAdReady => this.rewardedAdB != null && this.rewardedAdB.IsLoaded();

    public void SetAdapterConsent(string adapterClassName)
    {
        //Debug.LogError($"Setting Consent {adapterClassName}");

        //if (adapterClassName.Contains("tapjoy"))
        //{

        //    Tapjoy.client.SetUserConsent("1");
        //}

        if (adapterClassName.Contains("applovin"))
        {
      //      AppLovin.SetHasUserConsent(true);
        }

        else if (adapterClassName.Contains("unity"))
        {
    //        UnityAds.SetGDPRConsentMetaData(true);
        }

        //if(adapterClassName.Contains("vungle"))
        //{
        //    Vungle.UpdateConsentStatus(VungleConsent.ACCEPTED, "1.0.0");
        //}
    }

    public override void RequestBannerAd(BannerPosition bannerPosition = BannerPosition.Bottom)
    {
        //if (GameplayDataManager.Instance.gameplayData.noAdsPurchased)
        //    return;

        if (!this.isAdmobInitialized || !Constant.UtilityData.IsInternetAvailable)
            return;

        this.requiredBannerPosition = bannerPosition;

        if (this.bannerAd != null)
        {
            this.bannerAd.Destroy();
        }

        this.bannerAd = null;
        this.bannerAdStatus = AdStatus.NotLoading;

        AdPosition adPosition = (AdPosition)((int)bannerPosition);
        this.bannerAd = new BannerView(this.bannerID, AdSize.Banner, adPosition);
        AdRequest bannerRequest = new AdRequest.Builder().Build();

        this.bannerAd.LoadAd(bannerRequest);

        this.bannerAd.OnAdLoaded += this.HandleOnBanner_AdLoaded;
        this.bannerAd.OnAdFailedToLoad += this.HandleOnBanner_FailedToLoad;
        this.bannerAd.OnAdClosed += this.HandleOnBanner_AdClosed;
        this.bannerAd.OnAdClosed += this.HandleOnBanner_AdClosed;
        this.bannerAd.OnAdOpening += this.HandleOnBanner_AdOpened;

        this.bannerAdStatus = AdStatus.Loading;
 //       GALogger.LogAdmobBannerEvent(AdEventType.Request);
   //     this.BannerAdEvent(GAAdAction.Request);
   //     GameAnalyticsILRD.SubscribeAdMobImpressions(this.bannerID, this.bannerAd);
    }

    public override void RequestInterstitial()
    {
        //if (GameplayDataManager.Instance.gameplayData.noAdsPurchased)
        //    return;

        if (this.IsInterstitialAdReady || this.isInterstitialRequestGiven || !this.isAdmobInitialized || !Constant.UtilityData.IsInternetAvailable)
            return;

        if(this.interstitialAd!=null)
            this.interstitialAd.Destroy();

        this.interstitialAd = new InterstitialAd(this.interstitialID);

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;
        this.interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
        this.interstitialAd.OnAdClosed += InterstitialAd_OnAdClosed;

        this.interstitialAd.LoadAd(request);

        this.isInterstitialRequestGiven = true;

     //   GALogger.LogAdmobInterstitialEvent(AdEventType.Request);
     //   this.InterstitialAdEvent(GAAdAction.Request);
     //   GameAnalyticsILRD.SubscribeAdMobImpressions(this.interstitialID, this.interstitialAd);
    }

    public override void RequestRewardedVideo()
    {
        if (this.IsRewardedAdReady || this.isRewardedRequestGiven || !this.isAdmobInitialized || !Constant.UtilityData.IsInternetAvailable)
            return;

            this.rewardedAd = new RewardedAd(this.rewardedID);

            AdRequest request = new AdRequest.Builder().Build();

            this.rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;
            this.rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
            this.rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow;
            this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
            this.rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;

            this.rewardedAd.LoadAd(request);

        this.isRewardedRequestGiven = true;

      //  GALogger.LogAdmobRewardedEvent(AdEventType.Request);
     //   this.RewardedAdEvent(GAAdAction.Request);
      //  GameAnalyticsILRD.SubscribeAdMobImpressions(this.rewardedID, this.rewardedAd);
    }

    public void HandleOnBanner_AdLoaded(object sender, EventArgs args)
    {
        if (this.OnBannerLoaded != null)
            this.OnBannerLoaded();

        this.bannerAdStatus = AdStatus.Loaded;
      //  GALogger.LogAdmobBannerEvent(AdEventType.Loaded);
    }

    public override void CloseBanner()
    {
        if (this.bannerAd != null)
        {
            this.bannerAd.Destroy();
        }

        this.bannerAd = null;
        this.bannerAdStatus = AdStatus.NotLoading;
        //this.RequestBannerAd(false);
    }

    public override void ShowBannerAd(Action onBannerShowAction)
    {
        //if (this.gameplayDataManager.gameplayData.noAdsPurchased)
        //    return;

        if (this.IsBannerAdReady && this.bannerAdStatus != AdStatus.Shown)
        {
            this.bannerAd.Show();
      //      GALogger.LogAdmobBannerEvent(AdEventType.Show);
        //    this.BannerAdEvent(GAAdAction.Show);
            this.bannerAdStatus = AdStatus.Shown;

            if (onBannerShowAction != null)
                onBannerShowAction();
        }
        else
        {
            MobileAdsEventExecutor.ExecuteInUpdate(()=> 
            {
                this.RequestBannerAd(this.requiredBannerPosition);
            });
        }
    }

    public void HandleOnBanner_FailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        this.bannerAdStatus = AdStatus.NotLoading;
//        GALogger.LogAdmobBannerEvent(AdEventType.NotLoaded);
  //      this.BannerAdEvent(GAAdAction.FailedShow);
    }

    public void HandleOnBanner_AdOpened(object sender, EventArgs args)
    {
        this.bannerAdStatus = AdStatus.Shown;
    }

    public void HandleOnBanner_AdClosed(object sender, EventArgs args)
    {
        this.bannerAdStatus = AdStatus.NotLoading;
     //    GALogger.LogAdmobBannerEvent(AdEventType.AdClose);
    }

    public void HandleOnBanner_AdLeavingApplication(object sender, EventArgs args)
    {

    }


private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        if(this.rewardedDelegate!=null)
        {
       //     GALogger.LogAdmobRewardedEvent(AdEventType.Rewarded);
            MobileAdsEventExecutor.ExecuteInUpdate(this.rewardedDelegate);
         //   this.RewardedAdEvent(GAAdAction.RewardReceived);
        }
    }

    private void RewardedAd_OnAdLoaded(object sender, EventArgs e)
    {
        this.isRewardedRequestGiven = false;
      //  GALogger.LogAdmobRewardedEvent(AdEventType.Loaded);
      //  this.RewardedAdEvent(GAAdAction.Loaded);
    }

    private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        this.isRewardedRequestGiven = false;
      //  GALogger.LogAdmobRewardedEvent(AdEventType.FaildToShow);
        MobileAdsEventExecutor.ExecuteInUpdate(this.RequestRewardedVideo);
     //   this.RewardedAdEvent(GAAdAction.FailedShow);
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        this.isRewardedRequestGiven = false;
    //    GALogger.LogAdmobRewardedEvent(AdEventType.NotLoaded);
    }

    private void RewardedAd_OnAdClosed(object sender, EventArgs e)
    {
        this.isRewardedRequestGiven = false;
      //  GALogger.LogAdmobRewardedEvent(AdEventType.AdClose);
        MobileAdsEventExecutor.ExecuteInUpdate(this.RequestRewardedVideo);
     //   this.RewardedAdEvent(GAAdAction.Clicked);
    }

    private void InterstitialAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        this.isInterstitialRequestGiven = false;
      //  GALogger.LogAdmobInterstitialEvent(AdEventType.FaildToShow);
      //  this.InterstitialAdEvent(GAAdAction.FailedShow);
    }

    private void InterstitialAd_OnAdClosed(object sender, EventArgs e)
    {
        if (this.isLinkerAd)
        {
            if (this.rewardedDelegate != null)
            {
                MobileAdsEventExecutor.ExecuteInUpdate(this.rewardedDelegate);
            }
            this.isLinkerAd = false;
        }

        MobileAdsEventExecutor.ExecuteInUpdate(this.RequestInterstitial);
        this.isInterstitialRequestGiven = false;
     //   GALogger.LogAdmobInterstitialEvent(AdEventType.AdClose);
     //   this.InterstitialAdEvent(GAAdAction.Clicked);
    }

    private void InterstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        this.isInterstitialRequestGiven = false;
      //  GALogger.LogAdmobInterstitialEvent(AdEventType.NotLoaded);
    }

    private void InterstitialAd_OnAdLoaded(object sender, EventArgs e)
    {
        this.isInterstitialRequestGiven = false;

      //  GALogger.LogAdmobInterstitialEvent(AdEventType.Loaded);
     //   this.InterstitialAdEvent(GAAdAction.Loaded);
    }

    public override void RequestRewardedInterstitial()
    {
        
    }


    public override void ShowInterstitial()
    {

#if UNITY_EDITOR
        return;
#endif

        //if (this.gameplayDataManager.gameplayData.noAdsPurchased)
        //    return;

        if (this.IsInterstitialAdReady)
        {
            this.isInterstitialRequestGiven = false;
            this.interstitialAd.Show();
       //     GALogger.LogAdmobInterstitialEvent(AdEventType.Show);
    //        this.InterstitialAdEvent(GAAdAction.Show);
        }

        else
        {
  //          GALogger.LogAdmobInterstitialEvent(AdEventType.NotLoaded);
        }
    }

    public override void ShowRewardedVideo(Action rewardedDelegate, Action onRewardedVideoShown, Action onRewardedVideoNotShown, string rewardedPlacement)
    {
#if UNITY_EDITOR
        rewardedDelegate();
        return;
#endif

        this.rewardedDelegate = rewardedDelegate;

        if (this.rewardedAd.IsLoaded())
        {
            this.isRewardedRequestGiven = false;
            this.rewardedAd.Show();
  //          GALogger.LogAdmobRewardedEvent(AdEventType.Show);
       //     this.RewardedAdEvent(GAAdAction.Show);
      //      GALogger.LogGAEvent($"RewardedAdPlace:{rewardedPlacement}");
        }

        else
        {
            if(this.IsInterstitialAdReady)
            {
                this.isLinkerAd = true;
                this.ShowInterstitial();
            }

            MobileAdsEventExecutor.ExecuteInUpdate(this.RequestRewardedVideo);
       //     ToastManager.Instance.ShowToastMessage("<color=yellow>Please Try Again</color>,Ad Not Loaded");
        }
    }

    public override void ShowRewardedInterstitial(Action rewardedDelegate, Action onRewardedInterstitialShown, Action onRewardedInterstitialNotShown)
    {

    }


#region AdEvents

    //public void BannerAdEvent(GAAdAction adAction)
    //{
    //  //  GALogger.AdEvent(adAction, GAAdType.Banner, this.bannerID);
    //}

    //public void RewardedAdEvent(GAAdAction adAction)
    //{
    //  //  GALogger.AdEvent(adAction, GAAdType.RewardedVideo, this.rewardedID);
    //}

    //public void InterstitialAdEvent(GAAdAction adAction)
    //{
    //    //GALogger.AdEvent(adAction, GAAdType.Interstitial, this.interstitialID);
    //}
#endregion
}
